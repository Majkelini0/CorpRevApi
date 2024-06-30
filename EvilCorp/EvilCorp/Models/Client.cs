using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdClient))]
public class Client
{
    [Key]
    public int IdClient { get; set; }

    [MaxLength(300)]
    public string Address { get; set; } = null!;
    
    [MaxLength(50)]
    public string PhoneNum { get; set; } = null!;
    
    [MaxLength(200)]
    public string Email { get; set; } = null!;
    
    [MaxLength(1)]
    public string IsDeleted { get; set; } = null!;
    
    [MaxLength(1)]
    public string PrevClient { get; set; } = null!;
    
    // reference navigation to dependent entity
    public Individual? Individual { get; set; }
    
    // reference navigation to dependent entity
    public Company? Company { get; set; }
    
    // 
    public List<SingleSale> SingleSales { get; set; } = new();
}