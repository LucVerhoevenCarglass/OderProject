using System;
using System.Collections.Generic;
using Order_domain.Orders;

namespace Order_service.Orders
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);

        IEnumerable<Order> GetOrdersForCustomer(Guid customerId);

        Order ReorderOrder(Guid orderId);

        IEnumerable<Order> GetAllOrders(bool onlyIncludeShippableToday);
    }
}