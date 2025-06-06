using Randim.Common.Shared.Responses;

namespace Randim.UserService.DataAccess.Interfaces;

public interface IUserFriendshipManager
{
    Task<BaseResponse<bool>> AddFriend(int userId, int friendId);
    Task<BaseResponse<bool>> RemoveFriend(int userId, int friendId);
    Task<BaseResponse<bool>> AcceptFriend(int userId, int friendId);
    Task<BaseResponse<bool>> RejectFriend(int userId, int friendId);
}
