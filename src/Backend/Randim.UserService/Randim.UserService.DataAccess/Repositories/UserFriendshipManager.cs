using System.Security.Claims;
using Dapper;
using Randim.Common.DataAccess.Factory;
using Randim.Common.DataAccess.Repository;
using Randim.UserService.DataAccess.Interfaces;
using Randim.UserService.Models.Models;
using Randim.UserService.SDK.DTOs;

namespace Randim.UserService.DataAccess.Repositories;

public class UserFriendshipManager(IDbConnectionFactory connectionFactory)
    : BaseRepository(connectionFactory),
        IUserFriendshipManager
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public Task<bool> CreateUser(ClaimsPrincipal user)
    {
        var userClaims = user.Claims.ToDictionary(x => x.Type, x => x.Value);
        Console.WriteLine(userClaims);
        return new Task<bool>(() =>
        {
            return true;
        });
    }

    public async Task<bool> AddFriend(
        FriendRequestDto friendRequest,
        CancellationToken cancellationToken = default
    )
    {
        if (friendRequest.FriendRequesterId == friendRequest.FriendReceiverId)
            return false;
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var sqlExists =
            @" 
    select count(*)=2 from appusers where id in (@userid1,@userid2)
";
        var usersExists = await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sqlExists,
                new
                {
                    userid1 = friendRequest.FriendRequesterId,
                    userid2 = friendRequest.FriendReceiverId,
                },
                cancellationToken: cancellationToken
            )
        );
        if (!usersExists)
            return false;
        var (userid1, userid2) = await NormalizingUserIds(
            friendRequest.FriendRequesterId,
            friendRequest.FriendReceiverId
        );

        var sql =
            @"
            INSERT INTO FriendRequests(user_id_1,user_id_2,friend_requester_id,accepted, created_at,
                                       updated_at, is_deleted)
        VALUES(@user_id_1, @user_id_2, @friend_requester_id, @accepted, @created_at, @updated_at, @is_deleted)";
        var res = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    user_id_1 = userid1,
                    user_id_2 = userid2,
                    friend_requester_id = friendRequest.FriendRequesterId,
                    accepted = false,
                    created_at = DateTime.Now,
                    updated_at = (DateTime?)null,
                    is_deleted = false,
                },
                cancellationToken: cancellationToken
            )
        );
        return res > 0;
    }

    public async Task<IEnumerable<AppUser>?> GetFriends(
        int userId,
        CancellationToken cancellationToken = default
    )
    {
        var exists = await ExistsAsync("AppUsers", userId, cancellationToken);
        if (!exists)
            return null;
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var sql = new CommandDefinition(
            @"SELECT * FROM confirmed_friends WHERE user_id_1 = @userid1 OR user_id_2 = @userid2",
            new { userid1 = userId, userid2 = userId },
            cancellationToken: cancellationToken
        );
        var res = await connection.QueryAsync<ConfirmedFriends>(sql);
        if (!res.Any())
            return Enumerable.Empty<AppUser>();
        var userIds = res.Select(x => x.UserId1 == userId ? x.UserId2 : x.UserId1).ToArray();
        var users = await connection.QueryAsync<AppUser>(
            new CommandDefinition(
                @"SELECT * FROM appusers WHERE id = ANY(@Ids) ",
                new { Ids = userIds },
                cancellationToken: cancellationToken
            )
        );
        return users;
    }

    public async Task<bool> RemoveFriend(
        int userId,
        int friendId,
        CancellationToken cancellationToken = default
    )
    {
        if (userId == friendId)
            return false;
        var (userid1, userid2) = await NormalizingUserIds(userId, friendId);
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var connectedSql = new CommandDefinition(
            @"SELECT EXISTS(SELECT 1 FROM confirmed_friends WHERE user_id_1 = @userid1 AND user_id_2 = @userid2)",
            new { userid1, userid2 },
            cancellationToken: cancellationToken
        );
        var connected = await connection.ExecuteScalarAsync<bool>(connectedSql);
        if (!connected)
            return false;

        var sql =
            @"DELETE FROM confirmed_friends WHERE user_id_1 = @userid1 AND user_id_2 = @userid2";
        var res = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new { userid1, userid2 },
                cancellationToken: cancellationToken
            )
        );
        return res > 0;
    }

    public async Task<bool> AcceptFriend(
        int friendReceiverId,
        int friendRequesterId,
        CancellationToken cancellationToken = default
    )
    {
        var requesterExists = await ExistsAsync("AppUsers", friendRequesterId, cancellationToken);
        var receiverExists = await ExistsAsync("AppUsers", friendReceiverId, cancellationToken);
        if (!requesterExists || !receiverExists)
            return false;
        var (userid1, userid2) = await NormalizingUserIds(friendRequesterId, friendReceiverId);
        var connected = await ConnectedAsync(userid1, userid2, cancellationToken);
        if (!connected)
            return false;
        ConfirmedFriends confirmedFriends = new()
        {
            UserId1 = userid1,
            UserId2 = userid2,
            Confirmed = true,
            CreatedAt = DateTime.Now,
        };

        var sql =
            @"INSERT INTO confirmed_friends(user_id_1, user_id_2, confirmed, created_at, updated_at, is_deleted)
                    VALUES(@UserId1, @UserId2, @Confirmed, @CreatedAt, @UpdatedAt, @IsDeleted)";
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var transaction = connection.BeginTransaction();
        var deleteUserFriendRequest = new CommandDefinition(
            @"DELETE FROM friendrequests WHERE user_id_1 = @userid1 AND user_id_2 = @userid2",
            new { userid1, userid2 },
            transaction,
            cancellationToken: cancellationToken
        );

        var res = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                confirmedFriends,
                cancellationToken: cancellationToken,
                transaction: transaction
            )
        );
        var deleteConfirmedFriends = await connection.ExecuteAsync(deleteUserFriendRequest);
        transaction.Commit();
        return res > 0 && deleteConfirmedFriends > 0;
    }

    public async Task<bool> RejectFriend(
        int userId,
        int friendId,
        CancellationToken cancellationToken = default
    )
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var (userid1, userid2) = await NormalizingUserIds(userId, friendId);
        var connectedSql = new CommandDefinition(
            @"SELECT EXISTS(SELECT 1 FROM friendrequests WHERE user_id_1 = @userid1 AND user_id_2 = @userid2)",
            new { userid1, userid2 },
            cancellationToken: cancellationToken
        );
        var connected = await connection.ExecuteScalarAsync<bool>(connectedSql);
        if (!connected)
            return false;
        var sql = @"DELETE FROM friendrequests WHERE user_id_1 = @userid1 AND user_id_2 = @userid2";
        var res = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new { userid1, userid2 },
                cancellationToken: cancellationToken
            )
        );
        return res > 0;
    }

    private async Task<bool> ConnectedAsync(
        int userid1,
        int userid2,
        CancellationToken cancellationToken
    )
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var sql =
            @"SELECT EXISTS(SELECT 1 FROM friendrequests WHERE user_id_1 = @userid1 AND user_id_2 = @userid2)";
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new { userid1, userid2 },
                cancellationToken: cancellationToken
            )
        );
    }
}
