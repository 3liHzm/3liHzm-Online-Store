
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Domain.Models;



namespace Shop.Database
{
   public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Stock> Stock { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Catagories> Categories { get; set; } 
        public DbSet<OrderStock> OrderStocks { get; set; }
        public DbSet<StockOnHold> StockOnHolds { get; set; }
        public DbSet<ImgGallary> ProductImgGallary { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) //we override to give orderproduct class a primary key so it have two keys
        {
            base.OnModelCreating(modelBuilder);// here we shold to baas to the base class the modelbuldir
            modelBuilder.Entity<OrderStock>().HasKey(s => new { s.StockId, s.OrderId });
        }

    }
}


