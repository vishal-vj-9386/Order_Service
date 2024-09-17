using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order_Service.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Service.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Customer>().HasData(
                new { Id = 1, Name = "Vishal Jaiswal", Email = "vishal.vj9386@gmail.com" }
            );

            modelBuilder.Entity<Product>().HasData(
                new { Id = 1, Name = "IPhone 15", Price = 85000 },
                new { Id = 2, Name = "Samsung Galaxy S24", Price = 78000 },
                new { Id = 3, Name = "OnePlus Nord CE 3 Lite", Price = 18000 },
                new { Id = 4, Name = "Google Pixle", Price = 50000 },
                new { Id = 5, Name = "HP Probook", Price = 75000 },
                new { Id = 6, Name = "Lenevo Ideapad", Price = 59000 }
            );
        }
    }
}
