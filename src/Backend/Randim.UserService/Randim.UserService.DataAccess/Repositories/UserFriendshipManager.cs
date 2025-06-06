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

    public async Task<bool> AddFriend(
        FriendRequestDto friendRequest,
        CancellationToken cancellationToken = default
    )
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var userExists = await ExistsAsync(
            "AppUsers",
            friendRequest.FriendSenderId,
            cancellationToken
        );
        var friendExists = await ExistsAsync(
            "AppUsers",
            friendRequest.FriendReceiverId,
            cancellationToken
        );
        if (!userExists || !friendExists)
            return false;
        var sql =
            @"
            INSERT INTO FriendRequests(FriendReceiverId, FriendSenderId,Accepted, CreatedAt,
                                       UpdatedAt, IsDeleted)
        VALUES(@FriendReceiverId, @FriendSenderId, @Accepted, @CreatedAt, @UpdatedAt, @IsDeleted)";
        var res = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    FriendReceiverId = friendRequest.FriendReceiverId,
                    FriendSenderId = friendRequest.FriendSenderId,
                    Accepted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = (DateTime?)null,
                    IsDeleted = false,
                },
                cancellationToken: cancellationToken
            )
        );
        return res > 0;
    }

    public Task<bool> RemoveFriend(
        int userId,
        int friendId,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
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
        var connected = await ConnectedAsync(
            "FriendRequests",
            friendRequesterId,
            friendReceiverId,
            cancellationToken
        );
        if (!connected)
            return false;
        ConfirmedFriends confirmedFriends = new()
        {
            UserId1 = friendRequesterId,
            UserId2 = friendReceiverId,
            Confirmed = true,
            CreatedAt = DateTime.Now,
        };

        var sql =
            @"INSERT INTO ConfirmedFriends(UserId1, UserId2, Confirmed, CreatedAt, UpdatedAt, IsDeleted)
                    VALUES(@UserId1, @UserId2, @Confirmed, @CreatedAt, @UpdatedAt, @IsDeleted)";
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var transaction = connection.BeginTransaction();
        var deleteUserFriendRequest = new CommandDefinition(
            @"DELETE FROM FriendRequests WHERE FriendReceiverId = @FriendReceiverId AND FriendSenderId = @FriendSenderId",
            new { FriendReceiverId = friendRequesterId, FriendSenderId = friendReceiverId },
            transaction: transaction,
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

    public Task<bool> RejectFriend(
        int userId,
        int friendId,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }
}
