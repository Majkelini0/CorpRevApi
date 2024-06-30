using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class Software
{
    public int IdSoftware { get; set; }

    public string Name { get; set; } = null!;

    public string SoftInfo { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string VerInfo { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<SingleSale> SingleSales { get; set; } = new List<SingleSale>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
