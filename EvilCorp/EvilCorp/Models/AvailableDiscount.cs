using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(SoftwareId), nameof(DiscountId))]
public class AvailableDiscount
{
    [ForeignKey(nameof(SoftwareId))]
    public Software Software { get; set; } = null!;
    public int SoftwareId { get; set; }
    
    [ForeignKey(nameof(DiscountId))]
    public Discount Discount { get; set; } = null!;
    public int DiscountId { get; set; }
}