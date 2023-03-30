namespace WebApi.Models;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}