﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackendComputer.Models.Data
{
    public partial class ComputerdbContext : DbContext
    {
        public ComputerdbContext()
        {
        }

        public ComputerdbContext(DbContextOptions<ComputerdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<DetailsProducts> DetailsProducts { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<StatusTransport> StatusTransport { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<Transport> Transport { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AdminNme)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetailsProducts>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.IdProductsDetails).HasColumnName("id_ProductsDetails");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.MoreDetail)
                    .IsUnicode(false)
                    .HasColumnName("More_Detail");

                entity.HasOne(d => d.IdProductsDetailsNavigation)
                    .WithMany(p => p.DetailsProducts)
                    .HasForeignKey(d => d.IdProductsDetails)
                    .HasConstraintName("FK_DetailsProducts_Products1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.OrderUserNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderUser)
                    .HasConstraintName("FK_Order_User1");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOrder).HasColumnName("Id_Order");

                entity.Property(e => e.IdProducts).HasColumnName("Id_Products");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.IdProductsNavigation)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.IdProducts)
                    .HasConstraintName("FK_OrderDetail_Products");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdOrderr).HasColumnName("Id_Orderr");

                entity.Property(e => e.PayDate).HasColumnType("date");

                entity.Property(e => e.PaySlipimage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOrderrNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.IdOrderr)
                    .HasConstraintName("FK_Payments_Order1");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DetailSpecifics)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdType).HasColumnName("Id_Type");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.PdStock).HasColumnName("pd_stock");

                entity.Property(e => e.ProductDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_Products_Type1");
            });

            modelBuilder.Entity<StatusTransport>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Stock1).HasColumnName("Stock");

                entity.Property(e => e.StockDate)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Stock)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_Stock_Products");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TitleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Transport)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_Transport_Order");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Transport)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_Transport_StatusTransport");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TitleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Title)
                    .HasConstraintName("FK_User_Title1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}