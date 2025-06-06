namespace Randim.UserService.Models.Models;

public class FriendRequest : BaseModel
{
    public required int FriendReceiverId { get; set; }
    public required int FriendSenderId { get; set; }
    public bool Accepted { get; set; } = false;
}
