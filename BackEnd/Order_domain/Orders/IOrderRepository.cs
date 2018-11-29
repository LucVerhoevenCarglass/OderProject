using System;
using System.Collections.Generic;

namespace Order_domain.Orders
{
    public interface IOrderRepository
    {
        Order Save(Order entity);

        Order Update(Order entity);

        //Dictionary<Guid, Order> GetAll();
        List<Order> GetAll();

        Order Get(Guid entityId);

        IEnumerable<Order> GetOrdersForCustomer(Guid customerId);

        void Reset();
    }
}