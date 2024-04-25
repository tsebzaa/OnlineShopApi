using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Domain.Models;

namespace Infrastructure;

public partial class DevSafeRossContext : DbContext
{
    public DevSafeRossContext()
    {
    }

    public DevSafeRossContext(DbContextOptions<DevSafeRossContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PRIMARY");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId).HasColumnType("int(11)");
            entity.Property(e => e.Amount).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.PaymentId, "fkOrderPayment");

            entity.HasIndex(e => e.UserId, "fkOrderUser");

            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.PaymentId).HasColumnType("int(11)");
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("fkOrderPayment");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fkOrderUser");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.HasIndex(e => e.OrderId, "fkOrderDetailsOrder");

            entity.HasIndex(e => e.ProductId, "fkOrderDetailsProduct");

            entity.Property(e => e.OrderDetailId).HasColumnType("int(11)");
            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fkOrderDetailsOrder");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fkOrderDetailsProduct");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PRIMARY");

            entity.ToTable("PaymentType");

            entity.Property(e => e.PaymentId).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("Product");

            entity.HasIndex(e => e.InventoryId, "fkProductInventory");

            entity.HasIndex(e => e.ProductCategoryId, "fkProductProductCategory");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.InventoryId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.ProductCategoryId).HasColumnType("int(11)");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Products)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("fkProductInventory");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("fkProductProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PRIMARY");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.Surname).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
