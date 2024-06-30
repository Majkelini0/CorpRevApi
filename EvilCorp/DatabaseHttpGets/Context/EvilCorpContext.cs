using DatabaseHttpGets.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHttpGets.Context;

public partial class EvilCorpContext : DbContext
{
    public EvilCorpContext()
    {
    }

    public EvilCorpContext(DbContextOptions<EvilCorpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Individual> Individuals { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SingleSale> SingleSales { get; set; }

    public virtual DbSet<Software> Softwares { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("Client");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.IsDeleted).HasMaxLength(1);
            entity.Property(e => e.PhoneNum).HasMaxLength(50);
            entity.Property(e => e.PrevClient).HasMaxLength(1);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.ClientId);

            entity.ToTable("Company");

            entity.Property(e => e.ClientId).ValueGeneratedNever();
            entity.Property(e => e.Krs).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(300);

            entity.HasOne(d => d.Client).WithOne(p => p.Company).HasForeignKey<Company>(d => d.ClientId);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.IdDiscount);

            entity.ToTable("Discount");

            entity.Property(e => e.Info).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Value).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Individual>(entity =>
        {
            entity.HasKey(e => e.ClientId);

            entity.ToTable("Individual");

            entity.Property(e => e.ClientId).ValueGeneratedNever();
            entity.Property(e => e.Fname)
                .HasMaxLength(200)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(200)
                .HasColumnName("LName");
            entity.Property(e => e.Pesel).HasMaxLength(11);

            entity.HasOne(d => d.Client).WithOne(p => p.Individual).HasForeignKey<Individual>(d => d.ClientId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.IdPayment);

            entity.ToTable("Payment");

            entity.HasIndex(e => e.SingleSaleId, "IX_Payment_SingleSaleId");

            entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.SingleSale).WithMany(p => p.Payments).HasForeignKey(d => d.SingleSaleId);
        });

        modelBuilder.Entity<SingleSale>(entity =>
        {
            entity.HasKey(e => e.IdSale);

            entity.ToTable("SingleSale");

            entity.HasIndex(e => e.ClientId, "IX_SingleSale_ClientId");

            entity.HasIndex(e => e.SoftwareId, "IX_SingleSale_SoftwareId");

            entity.Property(e => e.IsPaid).HasMaxLength(1);
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.UpdatesInfo).HasMaxLength(300);

            entity.HasOne(d => d.Client).WithMany(p => p.SingleSales).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.Software).WithMany(p => p.SingleSales).HasForeignKey(d => d.SoftwareId);
        });

        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(e => e.IdSoftware);

            entity.ToTable("Software");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.SoftInfo).HasMaxLength(300);
            entity.Property(e => e.VerInfo).HasMaxLength(300);
            entity.Property(e => e.Version).HasMaxLength(10);

            entity.HasMany(d => d.Discounts).WithMany(p => p.Softwares)
                .UsingEntity<Dictionary<string, object>>(
                    "AvailableDiscount",
                    r => r.HasOne<Discount>().WithMany().HasForeignKey("DiscountId"),
                    l => l.HasOne<Software>().WithMany().HasForeignKey("SoftwareId"),
                    j =>
                    {
                        j.HasKey("SoftwareId", "DiscountId");
                        j.ToTable("AvailableDiscount");
                        j.HasIndex(new[] { "DiscountId" }, "IX_AvailableDiscount_DiscountId");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
