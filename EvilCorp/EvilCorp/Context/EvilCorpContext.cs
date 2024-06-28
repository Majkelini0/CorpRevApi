using EvilCorp.Migrations;
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
    
    public DbSet<Discount> Discount { get; set; }
    
    //public DbSet<AvailableDiscounts> AvailableDiscounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Software>()
            .HasMany(e => e.Discounts)
            .WithMany(e => e.Softwares)
            .UsingEntity("AvailableDiscounts");
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
