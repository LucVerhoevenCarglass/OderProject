using System;
using System.Collections.Generic;
using Oder_infrastructure.builders;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;
using Order_domain.tests.Orders.OrderItems;

namespace Order_domain.tests.Orders
{
    public class OrderTestBuilder : Builder<Order>
    {
        private readonly Order.OrderBuilder _orderBuilder;

        private OrderTestBuilder(Order.OrderBuilder orderBuilder)
        {
            _orderBuilder = orderBuilder;
        }

        public static OrderTestBuilder AnEmptyOrder()
        {
            return new OrderTestBuilder(Order.OrderBuilder.Order());
        }

        public static OrderTestBuilder AnOrder()
        {
            return new OrderTestBuilder(Order.OrderBuilder.Order()
                .WithCustomerId(Guid.NewGuid())
                .WithOrderItems(new List<OrderItem>{OrderItemTestBuilder.AnOrderItem().Build(), OrderItemTestBuilder.AnOrderItem().Build()}));
        }

        public override Order Build()
        {
            return new Order(_orderBuilder);
        }

        public OrderTestBuilder WithId(Guid id)
        {
            _orderBuilder.WithId(id);
            return this;
        }

        public OrderTestBuilder WithOrderItems(params OrderItem[] orderItems)
        {
            _orderBuilder.WithOrderItems(orderItems);
            return this;
        }

        public OrderTestBuilder WithCustomerId(Guid customerId)
        {
            _orderBuilder.WithCustomerId(customerId);
            return this;
        }
    }
}
