using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdDiscount))]
public class Discount
{
    public int IdDiscount { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Name { get; set; } = null!;
    
    [MaxLength(300)]
    [Required]
    public string Info { get; set; } = null!;
    
    [Precision(5,2)]
    public decimal Value { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    //
    //public List<Software> Softwares { get; set; } = new List<Software>();
    public List<AvailableDiscount> AvailableDiscounts { get; set; } = new List<AvailableDiscount>();
}