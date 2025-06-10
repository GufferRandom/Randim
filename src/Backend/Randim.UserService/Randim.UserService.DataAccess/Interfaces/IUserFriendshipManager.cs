using System.Security.Claims;
using Randim.Common.Shared.Responses;
using Randim.UserService.Models.Models;
using Randim.UserService.SDK.DTOs;

namespace Randim.UserService.DataAccess.Interfaces;

public interface IUserFriendshipManager
{
    Task<bool> CreateUser(ClaimsPrincipal user);
    Task<bool> AddFriend(
        FriendRequestDto friendRequest,
        CancellationToken cancellationToken = default
    );
    Task<IEnumerable<AppUser>?> GetFriends(
        int userId,
        CancellationToken cancellationToken = default
    );
    Task<bool> RemoveFriend(
        int userId,
        int friendId,
        CancellationToken cancellationToken = default
    );
    Task<bool> AcceptFriend(
        int friendReceiverId,
        int friendRequesterId,
        CancellationToken cancellationToken = default
    );
    Task<bool> RejectFriend(
        int userId,
        int friendId,
        CancellationToken cancellationToken = default
    );
}
