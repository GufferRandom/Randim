using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Randim.UserService.DataAccess.Interfaces;
using Randim.UserService.Models.Models;
using Randim.UserService.SDK.DTOs;

namespace Randim.UserService.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(
    IUserFriendshipManager userFriendshipManager,
    IHttpContextAccessor httpContextAccessor
) : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "given_name");
        httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "given_name");
        return Ok(new { message = "CORS test OK", user.Value });
    }

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
            friendRequest.FriendRequesterId
        );
        return Ok(res);
    }

    [HttpGet("friends")]
    public async Task<IActionResult> GetFriends([FromQuery] int userId)
    {
        var res = await userFriendshipManager.GetFriends(userId);
        return Ok(res);
    }

    [HttpDelete("friends")]
    public async Task<IActionResult> RemoveFriend(
        [FromQuery] int userId,
        [FromQuery] int friendId,
        CancellationToken cancellationToken = default
    )
    {
        var res = await userFriendshipManager.RemoveFriend(userId, friendId, cancellationToken);
        return Ok(res);
    }
}
