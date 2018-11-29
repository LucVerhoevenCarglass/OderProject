using System;

namespace Order_domain.Orders.OrderItems.Events
{
    public class OrderItemCreatedEvent
    {
        internal event EventHandler<OrderItem> DirectoryChanged
        {
            add { directoryChanged += value; }
            remove { directoryChanged -= value; }
        }
        private EventHandler<OrderItem> directoryChanged;

        public OrderItem OrderItem { get; set; }

        public OrderItemCreatedEvent(OrderItem orderItem)
        //: base(orderItem)
        {
            OrderItem = orderItem;
        }
    }
}
