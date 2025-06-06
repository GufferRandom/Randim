namespace Randim.UserService.Models.Models;

public class ConfirmedFriends : BaseModel
{
    public required int UserId1 { get; set; }
    public required int UserId2 { get; set; }
    public bool Confirmed { get; set; } = true;
}
