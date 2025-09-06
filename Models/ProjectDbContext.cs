using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebAPICRUD.Models;

public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("billing_pkey");

            entity.ToTable("billing");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrdId).HasColumnName("ord_id");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Ord).WithMany(p => p.Billings)
                .HasForeignKey(d => d.OrdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("billing_ord_id_fkey");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Billings)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("billing_p_id_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.CustName)
                .HasMaxLength(100)
                .HasColumnName("cust_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrdId).HasColumnName("ord_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.OrdDate).HasColumnName("ord_date");

            entity.HasOne(d => d.Cust).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_cust_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.PName)
                .HasMaxLength(100)
                .HasColumnName("p_name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
