using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class Discount
{
    public int IdDiscount { get; set; }

    public string Name { get; set; } = null!;

    public string Info { get; set; } = null!;

    public decimal Value { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Software> Softwares { get; set; } = new List<Software>();
}
