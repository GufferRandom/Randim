using System.ComponentModel.DataAnnotations.Schema;

namespace Randim.UserService.Models.Models;

public class AppUser : BaseModel
{
    [Column("entra_id")]
    public required Guid EntraId { get; init; }
    public required string UserName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
