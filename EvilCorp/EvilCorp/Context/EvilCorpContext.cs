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
    
    public DbSet<Individual> Individual { get; set; }
    
    public DbSet<Company> Company { get; set; }
    
    public DbSet<Client> Client { get; set; }
    
    public DbSet<SingleSale> SingleSale { get; set; }
    
    public DbSet<Software> Software { get; set; }
    
    public DbSet<Payment> Payment { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
