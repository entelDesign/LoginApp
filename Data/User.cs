using System.ComponentModel.DataAnnotations;

namespace LoginApp.Data;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? AdSoyad { get; set; }
    public int RoleId { get; set; }
}