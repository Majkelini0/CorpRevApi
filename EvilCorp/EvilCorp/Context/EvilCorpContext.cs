using System;
using System.Collections.Generic;
using EvilCorp.Models;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Context;

public partial class EvilCorpContext : DbContext
{
    public EvilCorpContext()
    {
    }

    public EvilCorpContext(DbContextOptions<EvilCorpContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> User { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
