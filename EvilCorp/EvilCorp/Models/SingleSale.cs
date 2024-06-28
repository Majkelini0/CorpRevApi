using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdSale))]
public class SingleSale
{
    public int IdSale { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public DateTime ExpiresAt { get; set; }
    
    public DateTime? FulfilledAt { get; set; }
    
    [Precision(8,2)]
    public decimal Price { get; set; }
    
    [MaxLength(300)]
    [Required]
    public string UpdatesInfo { get; set; } = null!;
    
    [Required]
    public int SupportPeriod { get; set; } // Years
    
    [MaxLength(1)]
    [Required]
    public string IsPaid { get; set; } = null!;
    
    //
    public Client Client { get; set; } = null!;
    public int ClientId { get; set; }
    
    public Software Software { get; set; } = null!;
    public int SoftwareId { get; set; }
    
    //
    public List<Payment> Payments { get; set; } = null!;
}