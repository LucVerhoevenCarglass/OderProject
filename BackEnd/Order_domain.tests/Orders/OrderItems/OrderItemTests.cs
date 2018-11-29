using System;
using Order_domain.Items.Prices;
using Order_domain.Orders.OrderItems;
using Xunit;

namespace Order_domain.tests.Orders.OrderItems
{
    public class OrderItemTests
    {
        //Normally we would use a DateTimeProvider to be able to create a stub for the provided date.

        [Fact]
        public void GetShippingDate_givenItemWasInStock_thenReturnCurrentDatePlusOneDay()
        {
            OrderItem orderItem = new OrderItem(OrderItem.OrderItemBuilder.OrderItem()
                    .WithOrderedAmount(5)
                    .WithShippingDateBasedOnAvailableItemStock(10));

            Assert.Equal(DateTime.Now.AddDays(1).Date, orderItem.ShippingDate.Date);
        }

        [Fact]
        public void GetShippingDate_givenItemWasNotInStock_thenReturnCurrentDatePlusSevenDays()
        {
            OrderItem orderItem = new OrderItem(OrderItem.OrderItemBuilder.OrderItem()
                .WithOrderedAmount(5)
                .WithShippingDateBasedOnAvailableItemStock(4));

            Assert.Equal(DateTime.Now.AddDays(7).Date, orderItem.ShippingDate.Date);
        }

        [Fact]
        public void GetTotalPrice()
        {
            OrderItem orderItem = OrderItem.OrderItemBuilder.OrderItem()
                .WithItemPrice(Price.Create(new decimal(15)))
                .WithOrderedAmount(3)
                .Build();

            Price totalPrice = orderItem.GetTotalPrice();

            Assert.Equal(45, totalPrice.Amount);
        }
    }
}
