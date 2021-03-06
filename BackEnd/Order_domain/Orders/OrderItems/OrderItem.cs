﻿using System;
using Oder_infrastructure.builders;
using Order_domain.Items.Prices;

namespace Order_domain.Orders.OrderItems
{
    public sealed class OrderItem
    {
        //TODO : Nieuwe velden OderID, MainOrder
        //TODO : OrderId moet ingevuld worden MainOrder wordt via Contextgetoond
        public Guid OrderId { get; set; }
        public Order MainOrder { get; set; }
        public Guid ItemId { get; set; }
        public Price ItemPrice { get; set; }
        public int OrderedAmount { get; set; }
        public DateTime ShippingDate { get; set; }

        private OrderItem(){}

        public OrderItem(OrderItemBuilder orderItemBuilder)
        {
            OrderId = orderItemBuilder.OrderId;
            ItemId = orderItemBuilder.ItemId;
            ItemPrice = orderItemBuilder.ItemPrice;
            OrderedAmount = orderItemBuilder.OrderedAmount;
            ShippingDate = CalculateShippingDate(orderItemBuilder.AvailableItemStock);
        }

        private DateTime CalculateShippingDate(int availableItemStock)
        {
            if (availableItemStock - OrderedAmount >= 0)
            {
                return DateTime.Now.AddDays(1);
            }
            return DateTime.Now.AddDays(7);
        }

        public Price GetTotalPrice()
        {
            return Price.Create(ItemPrice.Amount * OrderedAmount);
        }

        public override string ToString()
        {
            return "OrderItem{" + "itemId=" + ItemId +
                    ", itemPrice=" + ItemPrice +
                    ", orderedAmount=" + OrderedAmount +
                    ", shippingDate=" + ShippingDate +
                    '}';
        }

        public sealed class OrderItemBuilder : Builder<OrderItem>
        {
            public Guid OrderId { get; set; }
            public Guid ItemId { get; set; }
            public Price ItemPrice { get; set; }
            public int OrderedAmount { get; set; }
            public int AvailableItemStock { get; set; }

            public static OrderItemBuilder OrderItem()
            {
                return new OrderItemBuilder();
            }

            public override OrderItem Build()
            {
                return new OrderItem(this);
            }

            public OrderItemBuilder WithOrderId(Guid orderId)
            {
                OrderId = orderId;
                return this;
            }

            public OrderItemBuilder WithItemId(Guid itemId)
            {
                ItemId = itemId;
                return this;
            }

            public OrderItemBuilder WithItemPrice(Price itemPrice)
            {
                ItemPrice = itemPrice;
                return this;
            }

            public OrderItemBuilder WithOrderedAmount(int orderedAmount)
            {
                OrderedAmount = orderedAmount;
                return this;
            }

            public OrderItemBuilder WithShippingDateBasedOnAvailableItemStock(int availableItemStock)
            {
                AvailableItemStock = availableItemStock;
                return this;
            }
        }
    }
}
