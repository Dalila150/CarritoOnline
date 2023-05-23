using CarritoOnline3.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarritoOnline3.Models.DAL
{
    public class EtnaContext : DbContext
    {
        public EtnaContext(DbContextOptions options) : base(options)
        {

        }

        
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        //overriden for Dadabase tables name to be singular, specify data, etc..
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");

            //modelBuilder.Entity<OrderDetail>().HasKey("ProductID");

            modelBuilder.Entity<Product>().Property("Price").HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property("TotalPrice").HasColumnType("decimal(18,2)");

        }

    }

    
}
