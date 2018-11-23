using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Order_domain.Customers;
using Order_domain.Items;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;

namespace Order_service.Data
{
    public partial class OrderContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;


        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public OrderContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.Entity<Customer>()
                .ToTable("Customers")
                .HasKey(customer => customer.Id);

            modelBuilder.Entity<Customer>()
                .OwnsOne(customer => customer.Email,
                    email =>
                    {
                        email.Property(prop => prop.LocalPart).HasColumnName("LocalPart");
                        email.Property(prop => prop.Complete).HasColumnName("Complete");
                        email.Property(prop => prop.Domain).HasColumnName("Domain");
                    })
                .OwnsOne(customer => customer.Address,
                    address =>
                    {
                        address.Property(prop => prop.Country).HasColumnName("Country");
                        address.Property(prop => prop.PostalCode).HasColumnName("PostalCode");
                        address.Property(prop => prop.StreetName).HasColumnName("StreetName");
                        address.Property(prop => prop.HouseNumber).HasColumnName("HouseNumber");
                    })
                .OwnsOne(customer => customer.PhoneNumber,
                    phoneNumber =>
                    {
                        phoneNumber.Property(prop => prop.CountryCallingCode).HasColumnName("CountryCallingCode");
                        phoneNumber.Property(prop => prop.Number).HasColumnName("Number");
                    });

            modelBuilder.Entity<Item>()
                .ToTable("Items")
                .HasKey(item => item.Id);

            modelBuilder.Entity<Item>()
                .OwnsOne(item => item.Price,
                    price => { price.Property(prop => prop.Amount).HasColumnName("Amount"); });

            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(order => order.Id);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.OrderCustomer)
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItems")
                .HasKey(orderitem => new {orderitem.OrderId,orderitem.ItemId});

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(orderitem => orderitem.ItemPrice,
                    price => { price.Property(prop => prop.Amount).HasColumnName("Amount"); });

            modelBuilder.Entity<OrderItem>()
                .HasOne(orderitem => orderitem.MainOrder)
                .WithMany()
                .HasForeignKey(order => order.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            
            SeedData(modelBuilder);

        }
    }
}
