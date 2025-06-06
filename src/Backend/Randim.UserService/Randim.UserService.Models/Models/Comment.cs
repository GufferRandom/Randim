namespace Randim.UserService.Models.Models;

public class Comment : BaseModel
{
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public ICollection<int>? ReactionsId { get; set; }
}
