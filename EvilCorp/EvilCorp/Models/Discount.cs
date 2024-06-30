using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdDiscount))]
public class Discount
{
    [Key]
    public int IdDiscount { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Name { get; set; } = null!;
    
    [MaxLength(300)]
    [Required]
    public string Info { get; set; } = null!;
    
    [Precision(5,2)]
    public decimal Value { get; set; } // In percent %
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    //
    public List<AvailableDiscount> AvailableDiscounts { get; set; } = new List<AvailableDiscount>();
}