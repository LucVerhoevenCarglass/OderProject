using System;
using Oder_infrastructure.builders;
using Order_domain.Items.Prices;
using Order_domain.Orders.OrderItems;

namespace Order_domain.tests.Orders.OrderItems
{
    public class OrderItemTestBuilder : Builder<OrderItem>
    {
        private readonly OrderItem.OrderItemBuilder _orderItemBuilder;

        private OrderItemTestBuilder(OrderItem.OrderItemBuilder orderItemBuilder)
        {
            _orderItemBuilder = orderItemBuilder;
        }

        public static OrderItemTestBuilder AnEmptyOrderItem()
        {
            return new OrderItemTestBuilder(OrderItem.OrderItemBuilder.OrderItem());
        }

        public static OrderItemTestBuilder AnOrderItem()
        {
            return new OrderItemTestBuilder(OrderItem.OrderItemBuilder.OrderItem()
                .WithItemId(Guid.NewGuid())
                .WithItemPrice(Price.Create(new decimal(49.95)))
                .WithOrderedAmount(10)
                .WithShippingDateBasedOnAvailableItemStock(15)
            );
        }

        
        public override OrderItem Build()
        {
            return _orderItemBuilder.Build();
        }

        public OrderItemTestBuilder WithItemId(Guid itemId)
        {
            _orderItemBuilder.WithItemId(itemId);
            return this;
        }

        public OrderItemTestBuilder WithItemPrice(Price itemPrice)
        {
            _orderItemBuilder.WithItemPrice(itemPrice);
            return this;
        }

        public OrderItemTestBuilder WithOrderedAmount(int orderedAmount)
        {
            _orderItemBuilder.WithOrderedAmount(orderedAmount);
            return this;
        }

        public OrderItemTestBuilder WithShippingDateBasedOnAvailableItemStock(int availableItemStock)
        {
            _orderItemBuilder.WithShippingDateBasedOnAvailableItemStock(availableItemStock);
            return this;
        }
    }
}