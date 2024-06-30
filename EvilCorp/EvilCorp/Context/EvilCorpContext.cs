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

    public DbSet<AvailableDiscount> AvailableDiscount { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Discount>().HasData(
            new Discount()
            {
                IdDiscount = 1,
                Name = "Black March",
                Info = "March discounts",
                Value = 3,
                StartDate = new DateTime(2024, 3, 1),
                EndDate = new DateTime(2024, 3, 31)
            },
            new Discount()
            {
                IdDiscount = 2,
                Name = "June Madness",
                Info = "June discounts",
                Value = 2.5m,
                StartDate = new DateTime(2024, 6, 10),
                EndDate = new DateTime(2024, 6, 30)
            },
            new Discount()
            {
                IdDiscount = 3,
                Name = "June for noobies",
                Info = "For all new customers",
                Value = 5,
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 6, 30)
            },
            new Discount()
            {
                IdDiscount = 4,
                Name = "July Madness",
                Info = "July discounts",
                Value = 3.9m,
                StartDate = new DateTime(2024, 7, 7),
                EndDate = new DateTime(2024, 7, 22)
            }
        );

        modelBuilder.Entity<Software>().HasData(
            new Software()
            {
                IdSoftware = 1,
                Name = "Lightroom",
                SoftInfo = "Image editing software",
                Version = "2023.9.9",
                VerInfo = "Not the latest version",
                Category = "Digital image processing software",
                Price = 5899.99m
            },
            new Software()
            {
                IdSoftware = 2,
                Name = "Lightroom",
                SoftInfo = "Image editing software",
                Version = "2024.0.0",
                VerInfo = "Not the latest version",
                Category = "Digital image processing software",
                Price = 5950.00m
            },
            new Software()
            {
                IdSoftware = 3,
                Name = "Lightroom",
                SoftInfo = "Image editing software",
                Version = "2024.1.0",
                VerInfo = "Latest version",
                Category = "Digital image processing software",
                Price = 5999.99m
            },
            new Software()
            {
                IdSoftware = 4,
                Name = "Photoshop",
                SoftInfo = "Graphic design software",
                Version = "2023.8.0",
                VerInfo = "Not the latest version",
                Category = "Graphic design software",
                Price = 5599.99m
            },
            new Software()
            {
                IdSoftware = 5,
                Name = "Photoshop",
                SoftInfo = "Graphic design software",
                Version = "2023.9.0",
                VerInfo = "Not the latest version",
                Category = "Graphic design software",
                Price = 5699.99m
            },
            new Software()
            {
                IdSoftware = 6,
                Name = "Photoshop",
                SoftInfo = "Graphic design software",
                Version = "2023.9.9",
                VerInfo = "Not the latest version",
                Category = "Graphic design software",
                Price = 5799.99m
            },
            new Software()
            {
                IdSoftware = 7,
                Name = "Photoshop",
                SoftInfo = "Graphic design software",
                Version = "2024.0.0",
                VerInfo = "Not the latest version",
                Category = "Graphic design software",
                Price = 5899.99m
            },
            new Software()
            {
                IdSoftware = 8,
                Name = "Photoshop",
                SoftInfo = "Graphic design software",
                Version = "2024.1.1",
                VerInfo = "Latest version",
                Category = "Graphic design software",
                Price = 6099.99m
            },
            new Software()
            {
                IdSoftware = 9,
                Name = "Paint 3D PRO",
                SoftInfo = "Graphic design software",
                Version = "2022.7.4",
                VerInfo = "Latest version",
                Category = "Simple Graphic design software",
                Price = 99.99m
            },
            new Software()
            {
                IdSoftware = 10,
                Name = "Illustrator",
                SoftInfo = "Graphic design software",
                Version = "2024.3.9",
                VerInfo = "Not the latest version",
                Category = "Graphic design software",
                Price = 3999.99m
            },
            new Software()
            {
                IdSoftware = 11,
                Name = "Illustrator",
                SoftInfo = "Graphic design software",
                Version = "2024.4.0",
                VerInfo = "Latest version",
                Category = "Simple Graphic design software",
                Price = 3999.99m
            }
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}