﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Order_service.Data;

namespace Order_service.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20181122075328_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Order_domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new { Id = new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e"), FirstName = "Tom", LastName = "Thompson" }
                    );
                });

            modelBuilder.Entity("Order_domain.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountOfStock");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new { Id = new Guid("712951fe-a3e7-4a1a-b09b-d8f8635b94f2"), AmountOfStock = 30, Description = "TestDescription", Name = "TestName" },
                        new { Id = new Guid("d54b368d-3a7d-4063-bb02-6294c680849e"), AmountOfStock = 30, Description = "TestDescription", Name = "TestName" }
                    );
                });

            modelBuilder.Entity("Order_domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new { Id = new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), CustomerId = new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e") }
                    );
                });

            modelBuilder.Entity("Order_domain.Orders.OrderItems.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("ItemId");

                    b.Property<Guid?>("OrderId1");

                    b.Property<int>("OrderedAmount");

                    b.Property<DateTime>("ShippingDate");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("OrderId1");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new { OrderId = new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), ItemId = new Guid("712951fe-a3e7-4a1a-b09b-d8f8635b94f2"), OrderedAmount = 10, ShippingDate = new DateTime(2018, 11, 23, 8, 53, 26, 363, DateTimeKind.Local) },
                        new { OrderId = new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), ItemId = new Guid("d54b368d-3a7d-4063-bb02-6294c680849e"), OrderedAmount = 10, ShippingDate = new DateTime(2018, 11, 23, 8, 53, 26, 368, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("Order_domain.Customers.Customer", b =>
                {
                    b.OwnsOne("Order_domain.Customers.Addresses.Address", "Address", b1 =>
                        {
                            b1.Property<Guid?>("CustomerId");

                            b1.Property<string>("Country")
                                .HasColumnName("Country");

                            b1.Property<string>("HouseNumber")
                                .HasColumnName("HouseNumber");

                            b1.Property<string>("PostalCode")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("StreetName")
                                .HasColumnName("StreetName");

                            b1.ToTable("Customers");

                            b1.HasOne("Order_domain.Customers.Customer")
                                .WithOne("Address")
                                .HasForeignKey("Order_domain.Customers.Addresses.Address", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { CustomerId = new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e"), Country = "Belgium", HouseNumber = "16A", PostalCode = "3000", StreetName = "Jantjesstraat" }
                            );
                        });

                    b.OwnsOne("Order_domain.Customers.Emails.Email", "Email", b1 =>
                        {
                            b1.Property<Guid?>("CustomerId");

                            b1.Property<string>("Complete")
                                .HasColumnName("Complete");

                            b1.Property<string>("Domain")
                                .HasColumnName("Domain");

                            b1.Property<string>("LocalPart")
                                .HasColumnName("LocalPart");

                            b1.ToTable("Customers");

                            b1.HasOne("Order_domain.Customers.Customer")
                                .WithOne("Email")
                                .HasForeignKey("Order_domain.Customers.Emails.Email", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { CustomerId = new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e"), Complete = "niels@mymail.be", Domain = "mymail.be", LocalPart = "niels" }
                            );
                        });

                    b.OwnsOne("Order_domain.Customers.PhoneNumbers.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid?>("CustomerId");

                            b1.Property<string>("CountryCallingCode")
                                .HasColumnName("CountryCallingCode");

                            b1.Property<string>("Number")
                                .HasColumnName("Number");

                            b1.ToTable("Customers");

                            b1.HasOne("Order_domain.Customers.Customer")
                                .WithOne("PhoneNumber")
                                .HasForeignKey("Order_domain.Customers.PhoneNumbers.PhoneNumber", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { CustomerId = new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e"), CountryCallingCode = "+32", Number = "484554433" }
                            );
                        });
                });

            modelBuilder.Entity("Order_domain.Items.Item", b =>
                {
                    b.OwnsOne("Order_domain.Items.Prices.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("ItemId");

                            b1.Property<decimal>("Amount")
                                .HasColumnName("Amount");

                            b1.ToTable("Items");

                            b1.HasOne("Order_domain.Items.Item")
                                .WithOne("Price")
                                .HasForeignKey("Order_domain.Items.Prices.Price", "ItemId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { ItemId = new Guid("712951fe-a3e7-4a1a-b09b-d8f8635b94f2"), Amount = 49.95m },
                                new { ItemId = new Guid("d54b368d-3a7d-4063-bb02-6294c680849e"), Amount = 49.95m }
                            );
                        });
                });

            modelBuilder.Entity("Order_domain.Orders.Order", b =>
                {
                    b.HasOne("Order_domain.Customers.Customer", "OrderCustomer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Order_domain.Orders.OrderItems.OrderItem", b =>
                {
                    b.HasOne("Order_domain.Orders.Order", "MainOrder")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Order_domain.Orders.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId1");

                    b.OwnsOne("Order_domain.Items.Prices.Price", "ItemPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderItemOrderId");

                            b1.Property<Guid>("OrderItemItemId");

                            b1.Property<decimal>("Amount")
                                .HasColumnName("Amount");

                            b1.ToTable("OrderItems");

                            b1.HasOne("Order_domain.Orders.OrderItems.OrderItem")
                                .WithOne("ItemPrice")
                                .HasForeignKey("Order_domain.Items.Prices.Price", "OrderItemOrderId", "OrderItemItemId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasData(
                                new { OrderItemOrderId = new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), OrderItemItemId = new Guid("712951fe-a3e7-4a1a-b09b-d8f8635b94f2"), Amount = 49.95m },
                                new { OrderItemOrderId = new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), OrderItemItemId = new Guid("d54b368d-3a7d-4063-bb02-6294c680849e"), Amount = 49.95m }
                            );
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
