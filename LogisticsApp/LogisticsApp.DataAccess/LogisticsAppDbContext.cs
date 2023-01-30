using LogisticsApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DataAccess
{
    public class LogisticsAppDbContext : DbContext
    {
        public LogisticsAppDbContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<Calculation> Calculations { get; set; }
        public DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // COURIERS TABLE
            modelBuilder.Entity<Courier>()
                .Property(x => x.CourierName)
                .IsRequired();
            // Data seeding
            modelBuilder.Entity<Courier>()
                .HasData(
                new Courier { Id = 1, CourierName = "Cargo4You" },
                new Courier { Id = 2, CourierName = "ShipFaster" },
                new Courier { Id = 3, CourierName = "MaltaShip" }
                );


            // VALIDATIONS TABLE
            modelBuilder.Entity<Validation>()
                .Property(x => x.ValidationType)
                .IsRequired();
            modelBuilder.Entity<Validation>()
                .Property(x => x.From)
                .IsRequired();
            modelBuilder.Entity<Validation>()
                .Property(x => x.To)
                .IsRequired();
            // Relations
            modelBuilder.Entity<Validation>()
                 .HasOne(x => x.Courier)
                 .WithMany(x => x.Validations)
                 .HasForeignKey(x => x.CourierId)
                 .IsRequired();
            // Data seeding
            modelBuilder.Entity<Validation>()
                .HasData(
                new Validation { Id = 1, CourierId = 1, ValidationType = 0, From = 0, To = 20},
                new Validation { Id = 2, CourierId = 1, ValidationType = 1, From = 0, To = 2000},
                new Validation { Id = 3, CourierId = 2, ValidationType = 0, From = 10, To = 30},
                new Validation { Id = 4, CourierId = 2, ValidationType = 1, From = 0, To = 1700},
                new Validation { Id = 5, CourierId = 3, ValidationType = 0, From = 0, To = 10},
                new Validation { Id = 6, CourierId = 3, ValidationType = 1, From = 500, To = 50000}
                );

            // CALCULATIONS TABLE
            modelBuilder.Entity<Calculation>()
                .Property(x => x.CalculationType)
                .IsRequired();
            modelBuilder.Entity<Calculation>()
                .Property(x => x.From)
                .IsRequired();
            modelBuilder.Entity<Calculation>()
                .Property(x => x.To)
                .IsRequired();
            modelBuilder.Entity<Calculation>()
                .Property(x => x.Cost);
            // Relations
            modelBuilder.Entity<Calculation>()
                .HasOne(x => x.Courier)
                .WithMany(x => x.Calculations)
                .HasForeignKey(x => x.CourierId);
            // Data seeding
            modelBuilder.Entity<Calculation>()
                .HasData(
                new Calculation { Id = 1, CourierId = 1, CalculationType = 0, From = 0, To = 2, Cost = 15 },
                new Calculation { Id = 2, CourierId = 1, CalculationType = 0, From = 2, To = 15, Cost = 18 },
                new Calculation { Id = 3, CourierId = 1, CalculationType = 0, From = 15, To = 20, Cost = 35 },
                new Calculation { Id = 4, CourierId = 1, CalculationType = 1, From = 0, To = 1000, Cost = 10 },
                new Calculation { Id = 5, CourierId = 1, CalculationType = 1, From = 1000, To = 2000, Cost = 20 },
                new Calculation { Id = 6, CourierId = 2, CalculationType = 0, From = 10, To = 15, Cost = 16.5 },
                new Calculation { Id = 7, CourierId = 2, CalculationType = 0, From = 15, To = 25, Cost = 36.5 },
                new Calculation { Id = 8, CourierId = 2, CalculationType = 0, From = 25, To = 1000, Cost = 40 },
                new Calculation { Id = 9, CourierId = 2, CalculationType = 1, From = 0, To = 1000, Cost = 11.99 },
                new Calculation { Id = 10, CourierId = 2, CalculationType = 1, From = 1000, To = 1700, Cost = 21.99 },
                new Calculation { Id = 11, CourierId = 3, CalculationType = 0, From = 10, To = 20, Cost = 16.99 },
                new Calculation { Id = 12, CourierId = 3, CalculationType = 0, From = 20, To = 30, Cost = 33.99 },
                new Calculation { Id = 13, CourierId = 3, CalculationType = 0, From = 30, To = 1000, Cost = 43.99 },
                new Calculation { Id = 14, CourierId = 3, CalculationType = 1, From = 0, To = 1000, Cost = 9.5 },
                new Calculation { Id = 15, CourierId = 3, CalculationType = 1, From = 1000, To = 2000, Cost = 19.5 },
                new Calculation { Id = 16, CourierId = 3, CalculationType = 1, From = 2000, To = 5000, Cost = 48.5 },
                new Calculation { Id = 17, CourierId = 3, CalculationType = 1, From = 5000, To = 50000, Cost = 147.5 }
                );

            // PACKAGES TABLE
            modelBuilder.Entity<Package>()
                .Property(x => x.Weight)
                .IsRequired();
            modelBuilder.Entity<Package>()
                .Property(x => x.Dimensions)
                .IsRequired();
            // Relations
            modelBuilder.Entity<Package>()
                .HasOne(x => x.Calculation)
                .WithMany(x => x.Packages)
                .HasForeignKey(x => x.CalculationId);
        }
    }
}
