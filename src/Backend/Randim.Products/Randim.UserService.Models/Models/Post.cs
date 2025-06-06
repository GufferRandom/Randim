namespace Randim.UserService.Models.Models;

public class Post : BaseModel
{
    public int UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public ICollection<int>? ReactionsId { get; set; }
    public ICollection<int>? CommentsId { get; set; }
}
