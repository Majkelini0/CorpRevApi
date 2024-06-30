using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class Company
{
    public int ClientId { get; set; }

    public string Name { get; set; } = null!;

    public string Krs { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
