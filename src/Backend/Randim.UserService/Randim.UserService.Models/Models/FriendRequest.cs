namespace Randim.UserService.Models.Models;

public class FriendRequest : BaseModel
{
    public required int UserId1 { get; set; }
    public required int UserId2 { get; set; }
    public required int FriendRequesterId { get; set; }
    public bool Accepted { get; set; } = false;
}
