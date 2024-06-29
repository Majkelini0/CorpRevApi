using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdSoftware))]
public class Software
{
    public int IdSoftware { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Name { get; set; } = null!;
    
    [MaxLength(300)]
    [Required]
    public string SoftInfo { get; set; } = null!;
    
    [MaxLength(10)]
    [Required]
    public string Version { get; set; } = null!;
    
    [MaxLength(300)]
    [Required]
    public string VerInfo { get; set; } = null!;
    
    [MaxLength(50)]
    [Required]
    public string Category { get; set; } = null!;
    
    [Precision(8,2)]
    public decimal Price { get; set; }
    
    //
    public List<SingleSale> SingleSales { get; set; } = new();
    
    //
    //public List<Discount> Discounts { get; set; } = new List<Discount>();
    public List<AvailableDiscount> AvailableDiscounts { get; set; } = new List<AvailableDiscount>();
}