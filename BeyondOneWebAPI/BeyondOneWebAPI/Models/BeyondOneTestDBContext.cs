using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeyondOneWebAPI.Models
{
    public partial class BeyondOneTestDBContext : DbContext
    {
        public BeyondOneTestDBContext()
        {
        }

        public BeyondOneTestDBContext(DbContextOptions<BeyondOneTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DBConnectioStr");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
