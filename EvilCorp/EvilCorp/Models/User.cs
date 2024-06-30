using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdUser))]
public class User
{
    [Key]
    public int IdUser { get; set; }
    
    public string Login { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public string Salt { get; set; } = null!;
    
    public string RefreshToken { get; set; } = null!;
    
    public DateTime RefreshTokenExpTime { get; set; }
}