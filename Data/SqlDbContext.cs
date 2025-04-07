using System;
using System.ComponentModel.DataAnnotations;
using CRM.Models;
using CRM.Models.JunctionModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace CRM.Data;

public class SqlDbContext : DbContext
{
  public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
  {

  }


  public DbSet<User> Users { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<Address> Addresses { get; set; }
  public DbSet<CartProduct> CartProducts { get; set; }



  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


    modelBuilder.Entity<Order>()
     .HasOne(o => o.Buyer)            //order has one buyer
     .WithMany(b => b.Orders)        //bUyer cxan have many orders
     .HasForeignKey(o => o.BuyerId)  //order has buyerId as a Foreign key 
     .OnDelete(DeleteBehavior.NoAction);



    modelBuilder.Entity<Address>()
    .HasOne(a => a.Buyer)             //address belong to one buyer
    .WithMany(b => b.Addresses)      //buyer can have many addresses
    .HasForeignKey(a => a.BuyerId)  //address has buyerId as a Foreign
    .OnDelete(DeleteBehavior.NoAction);


    modelBuilder.Entity<Product>()
    .HasOne(p => p.Seller)          // Product has one seller
    .WithMany(s => s.Products)      // Seller can have many products
    .HasForeignKey(p => p.SellerId)  // Product has sellerId as a Foreign key
    .OnDelete(DeleteBehavior.NoAction);




    modelBuilder.Entity<Cart>()
    .HasOne(c => c.Buyer)             //cart belongs to one buyer
    .WithOne(b => b.Cart)          //buyer can have one carts
    .HasForeignKey<Cart>(c => c.BuyerId)  //cart has buyerId as a Foreign
    .OnDelete(DeleteBehavior.NoAction);



    modelBuilder.Entity<CartProduct>()
   .HasKey(cp => cp.CartProductId);

   modelBuilder.Entity<CartProduct>()
   .HasOne(cp => cp.Cart)
   .WithMany(c => c.Products)
   .HasForeignKey(cp => cp.CartId);

   modelBuilder.Entity<CartProduct>()
   .HasOne(cp => cp.Product)
   .WithMany(p => p.Carts)
   .HasForeignKey(cp => cp.ProductId);




  }

}

