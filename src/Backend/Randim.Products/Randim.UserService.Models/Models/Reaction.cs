namespace Randim.UserService.Models.Models;

public class Reaction : BaseModel
{
    public int UserId { get; set; }
    public ReactType ReactType { get; set; }
}
