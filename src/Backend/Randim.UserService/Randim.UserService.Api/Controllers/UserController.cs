using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Randim.UserService.DataAccess.Interfaces;
using Randim.UserService.Models.Models;
using Randim.UserService.SDK.DTOs;

namespace Randim.UserService.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserFriendshipManager userFriendshipManager) : ControllerBase
{
    [HttpPost("addFriend")]
    public async Task<IActionResult> AddFriend([FromBody] FriendRequestDto friendRequest)
    {
        var res = await userFriendshipManager.AddFriend(friendRequest);
        return Ok(res);
    }

    [HttpPost("AcceptFriend")]
    public async Task<IActionResult> AcceptFriend([FromBody] FriendRequestDto friendRequest)
    {
        var res = await userFriendshipManager.AcceptFriend(
            friendRequest.FriendReceiverId,
            friendRequest.FriendSenderId
        );
        return Ok(res);
    }
}
