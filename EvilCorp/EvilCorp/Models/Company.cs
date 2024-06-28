using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(ClientId))]
public class Company
{
    [MaxLength(300)]
    public string Name { get; set; }
    
    [MaxLength(10)]
    public string Krs { get; set; }
    
    // foreign key property
    public int ClientId { get; set; }
    
    // reference navigation to principal entity
    public Client Client { get; set; } = null!;
}