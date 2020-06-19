using Microsoft.EntityFrameworkCore;
using OC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Data
{
    public class OnlineCosmeticsDBContext : DbContext
    {
        public OnlineCosmeticsDBContext() : base()
        { }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()   
                .UseSqlServer(@"Server=DESKTOP-MJGM6RT\SQLEXPRESS;" +
                              @"DataBase=OC;" +
                              @"Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()  
               .Property(c => c.NameCategory)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<Category>()
               .Property(c => c.Subcategory)
               .HasMaxLength(50);

            modelBuilder.Entity<Category>()
               .Property(c => c.Stars)
               .IsRequired();

            modelBuilder.Entity<Brand>()
                .Property(b => b.BrandName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Brand>()
                .Property(b => b.ManufacturerCountry)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasOne(m => m.Brand)
                .WithMany(d => d.Products)
                .HasForeignKey(m => m.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(m => m.Category)
                .WithMany(d => d.Products)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
