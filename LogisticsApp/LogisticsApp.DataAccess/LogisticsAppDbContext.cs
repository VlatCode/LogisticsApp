using LogisticsApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .Property(x => x.CourierName);

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
