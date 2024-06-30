using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string IsDeleted { get; set; } = null!;

    public string PrevClient { get; set; } = null!;

    public virtual Company? Company { get; set; }

    public virtual Individual? Individual { get; set; }

    public virtual ICollection<SingleSale> SingleSales { get; set; } = new List<SingleSale>();
}
