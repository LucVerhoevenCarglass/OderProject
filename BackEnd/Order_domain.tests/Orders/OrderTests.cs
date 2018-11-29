using Order_domain.Items.Prices;
using Order_domain.Orders;
using Order_domain.tests.Orders.OrderItems;
using Xunit;

namespace Order_domain.tests.Orders
{
    public class OrderTests
    {
        [Fact]
        public void GetTotalPrice_givenOrderWithOrderItems_thenTotalPriceIsSumOfPricesOfOrderItemsMultipliedByOrderedAmount()
        {
            Order order = OrderTestBuilder.AnOrder()
                .WithOrderItems(OrderItemTestBuilder.AnOrderItem().WithOrderedAmount(2).WithItemPrice(Price.Create(new decimal(40.50))).Build(),
                                OrderItemTestBuilder.AnOrderItem().WithOrderedAmount(1).WithItemPrice(Price.Create(new decimal(60.50))).Build(),
                                OrderItemTestBuilder.AnOrderItem().WithOrderedAmount(10).WithItemPrice(Price.Create(new decimal(25))).Build())
                .Build();

            Price totalPrice = order.GetTotalPrice();

            Assert.Equal(391.5, totalPrice.GetAmountAsFloat());
        }
    }
}
