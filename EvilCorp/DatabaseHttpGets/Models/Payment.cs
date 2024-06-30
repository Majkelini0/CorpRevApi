using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class Payment
{
    public int IdPayment { get; set; }

    public decimal Amount { get; set; }

    public int SingleSaleId { get; set; }

    public virtual SingleSale SingleSale { get; set; } = null!;
}
