using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain;
using Order.Domain.Customers;
using Order.Domain.Items;
using Order.Domain.Orders;
using Xunit;

namespace Order.Domain.UnitTests.Orders
{
    public class OrderTest
    {
        [Fact]
        public void GivenOrder_WhenSetShippingDateOnArticleWithEnoughStock_ThenDateSetToNextDay()
        {
            Item findItem = new Item()
            {
                Name = "test1",
                StockAmount = 5
            };
            ItemGroups itemGroups = new ItemGroups()
            {
                OrderAmount = 5
            };
            itemGroups.SetShippingDate(findItem);
            Assert.Equal(DateTime.Today.AddDays(1), itemGroups.OrderShippingDate);
            itemGroups.SetOrderAmount(findItem);
            Assert.Equal(5, itemGroups.OrderAmount);
        }

        [Fact]
        public void GivenOrder_WhenSetShippingDateOnArticleWithNotEnoughStock_ThenDateSetToNextWeekAndBackorderIsMade()
        {
            Item findItem = new Item()
            {
                Name = "test1",
                StockAmount = 5,
                ToOrder=2
            };
            ItemGroups itemGroups = new ItemGroups()
            {
                OrderAmount = 8
            };
            itemGroups.SetShippingDate(findItem);
            Assert.Equal(DateTime.Today.AddDays(7), itemGroups.OrderShippingDate);
            Assert.Equal(3, itemGroups.SetOrderAmount(findItem));
            Assert.Equal(8, itemGroups.OrderAmount);
            Assert.Equal(3, itemGroups.BackOrder);         
        }

        [Fact]
        public void GivenOrder_WhenMainOrderHasItemGroups_ThenCalculateTotalPrice()
        {
            MainOrder mainOrder= new MainOrder("ID");
            List<ItemGroups> listItemGroups = new List<ItemGroups>()
            {
                new ItemGroups()
                {
                    OrderAmount = 8,
                    Price = 20,
                },
                new ItemGroups()
                {
                    OrderAmount = 4,
                    Price = 5,
                }
            };
            mainOrder.ItemGroups = listItemGroups;
            Assert.Equal(180, mainOrder.CalculateTotalToPay());
        }
    }
}
