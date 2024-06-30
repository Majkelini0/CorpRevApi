using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class SingleSale
{
    public int IdSale { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? FulfilledAt { get; set; }

    public decimal Price { get; set; }

    public string UpdatesInfo { get; set; } = null!;

    public int AdditionalSupportPeriod { get; set; }

    public string IsPaid { get; set; } = null!;

    public int ClientId { get; set; }

    public int SoftwareId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Software Software { get; set; } = null!;
}
