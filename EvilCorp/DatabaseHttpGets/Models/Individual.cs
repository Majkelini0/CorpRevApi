using System;
using System.Collections.Generic;

namespace DatabaseHttpGets.Models;

public partial class Individual
{
    public int ClientId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Pesel { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
