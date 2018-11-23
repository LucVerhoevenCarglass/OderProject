using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Order_domain.Customers;
using Order_domain.Customers.Addresses;
using Order_domain.Customers.Emails;
using Order_domain.Customers.PhoneNumbers;
using Order_domain.Items;
using Order_domain.Items.Prices;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;

namespace Order_service.Data
{
    
    class OrderData
    {
        internal Customer Customer1;
        internal List<Item> ItemList;
        internal Order Order1;

        public OrderData()
        {
            AddressBuilder addressBuilder = new AddressBuilder()
                .WithCountry("Belgium")
                .WithHouseNumber("16A")
                .WithPostalCode("3000")
                .WithStreetName("Jantjesstraat");

            Email.EmailBuilder emailBuilder = new Email.EmailBuilder()
                .WithLocalPart("niels")
                .WithDomain("mymail.be")
                .WithComplete("niels@mymail.be");

            PhoneNumber.PhoneNumberBuilder phoneNumberBuilder = new PhoneNumber.PhoneNumberBuilder()
                .WithNumber("484554433")
                .WithCountryCallingCode("+32");

            Customer.CustomerBuilder custBuild = new Customer.CustomerBuilder()
                    .WithId(Guid.NewGuid())
                    .WithFirstname("Tom")
                    .WithLastname("Thompson")
                    .WithAddress(addressBuilder.Build())
                    .WithEmail(emailBuilder.Build())
                    .WithPhoneNumber(phoneNumberBuilder.Build());

            Customer1 = new Customer(custBuild);

            Item.ItemBuilder item1 = new Item.ItemBuilder()
                .WithId(Guid.NewGuid())
                .WithAmountOfStock(50)
                .WithDescription("Just a simple headphone")
                .WithName("Headphone")
                .WithPrice(Price.Create(new decimal(49.95)));

            Item.ItemBuilder item2 = new Item.ItemBuilder()
                .WithId(Guid.NewGuid())
                .WithAmountOfStock(50)
                .WithDescription("Just a simple micro")
                .WithName("Micro")
                .WithPrice(Price.Create(new decimal(22.95)));

            Order1 = new Order(new Order.OrderBuilder()
                .WithId(Guid.NewGuid()));


            OrderItem.OrderItemBuilder orderItem1 = new OrderItem.OrderItemBuilder()
                .WithOrderId(Order1.Id)
                .WithItemId(item1.Id)
                .WithItemPrice(item1.Price)
                .WithOrderedAmount(5);
            OrderItem.OrderItemBuilder orderItem2 = new OrderItem.OrderItemBuilder()
                .WithOrderId(Order1.Id)
                .WithItemId(item2.Id)
                .WithItemPrice(item2.Price)
                .WithOrderedAmount(5);

            Order1 = new Order(new Order.OrderBuilder()
                 .WithId(Guid.NewGuid())
                 .WithCustomerId(Customer1.Id)
                 .WithOrderItems(new List<OrderItem> { orderItem1.Build(),
                                                       orderItem2.Build()
                                                      })
            );

            ItemList = new List<Item>()
            {
                item1.Build(),
                item2.Build()
            };

        }
    }

    public partial class OrderContext
    {
            protected void SeedData(ModelBuilder modelBuilder)
            {
                var seedData = new OrderData();

                modelBuilder.Entity<Customer>(cust =>
                {
                    cust.HasData(new
                    {
                        Id = seedData.Customer1.Id,
                        FirstName = seedData.Customer1.FirstName,
                        LastName = seedData.Customer1.LastName
                    });
                    cust.OwnsOne(custAd => custAd.Address).HasData(new
                    {
                        CustomerId = seedData.Customer1.Id,
                        StreetName = seedData.Customer1.Address.StreetName,
                        HouseNumber = seedData.Customer1.Address.HouseNumber,
                        PostalCode = seedData.Customer1.Address.PostalCode,
                        Country = seedData.Customer1.Address.Country
                    });
                    cust.OwnsOne(custMail => custMail.Email).HasData(new
                    {
                        CustomerId = seedData.Customer1.Id,
                        LocalPart = seedData.Customer1.Email.LocalPart,
                        Domain = seedData.Customer1.Email.Domain,
                        Complete = seedData.Customer1.Email.Complete
                    });
                    cust.OwnsOne(custPhone => custPhone.PhoneNumber).HasData(new
                    {
                        CustomerId = seedData.Customer1.Id,
                        CountryCallingCode = seedData.Customer1.PhoneNumber.CountryCallingCode,
                        Number = seedData.Customer1.PhoneNumber.Number
                    });
                });


            //modelBuilder.Entity<Customer>().HasData(new List<Customer>()
            //{
            //    seedData.Customer1,
            //    seedData.Customer2,
            //    seedData.Customer3
            //}.ToArray());

            modelBuilder.Entity<Order>(ord =>
            {
                ord.HasData(new
                {
                    Id = seedData.Order1.Id,
                    CustomerId = seedData.Order1.CustomerId
                });
            });


            //modelBuilder.Entity<Item>().HasData(seedData.ItemList.ToArray());

            foreach (Item item in seedData.ItemList)
            {
                modelBuilder.Entity<Item>(oItem =>
                {
                    oItem.HasData(new
                    {
                        Id = item.Id,
                        AmountOfStock = item.AmountOfStock,
                        Description = item.Description,
                        Name = item.Name
                    });
                    oItem.OwnsOne(itemPrice => itemPrice.Price).HasData(new
                    {
                        ItemId = item.Id,
                        Amount = item.Price.Amount
                    });
                });
            }

            foreach (OrderItem item in seedData.Order1.OrderItems)
            {
                modelBuilder.Entity<OrderItem>(oItem =>
                {
                    oItem.HasData(new
                    {
                        OrderId = seedData.Order1.Id,
                        ItemId = item.ItemId,
                        OrderedAmount = item.OrderedAmount,
                        ShippingDate = item.ShippingDate,
                    });
                    oItem.OwnsOne(itemPrice => itemPrice.ItemPrice).HasData(new
                    {
                        OrderItemOrderId= seedData.Order1.Id,
                        OrderItemItemId = item.ItemId,
                        Amount = item.ItemPrice.Amount
                    });
                });
            }
        }
    }
   
}
