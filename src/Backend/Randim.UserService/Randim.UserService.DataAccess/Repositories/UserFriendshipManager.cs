using Randim.Common.Shared.Responses;
using Randim.UserService.DataAccess.Interfaces;

namespace Randim.UserService.DataAccess.Repositories;

public class UserFriendshipManager : IUserFriendshipManager
{
    public Task<BaseResponse<bool>> AddFriend(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> RemoveFriend(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> AcceptFriend(int userId, int friendId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> RejectFriend(int userId, int friendId)
    {
        throw new NotImplementedException();
    }
}
