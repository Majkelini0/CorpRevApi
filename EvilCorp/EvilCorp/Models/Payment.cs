using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(IdPayment))]
public class Payment
{
    public int IdPayment { get; set; }
    
    [Precision(8,2)]
    public decimal Amount { get; set; }
    
    //
    public SingleSale SingleSale { get; set; } = null!;
    public int SingleSaleId { get; set; }
}