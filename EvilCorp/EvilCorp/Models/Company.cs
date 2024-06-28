using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(ClientId))]
public class Company
{
    [MaxLength(300)]
    [Required]
    public string Name { get; set; } = null!;
    
    [MaxLength(10)]
    [Required]
    public string Krs { get; set; } = null!;
    
    // foreign key property
    public int ClientId { get; set; }
    
    // reference navigation to principal entity
    public Client Client { get; set; } = null!;
}