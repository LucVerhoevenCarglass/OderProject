using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.Orders
{
    public class OrderRepository : Repository<Order, OrderDatabase>, IOrderRepository
    {

        //private ApplicationEventPublisher eventPublisher;

        public OrderRepository(OrderDatabase database)
            : base(database)
        {
        }

        public Order Save(Order entity)
        {
            Order savedOrder = base.Save(entity);
            //savedOrder.OrderItems.ForEach(orderItem => eventPublisher.publishEvent(new OrderItemCreatedEvent(orderItem)));
            return savedOrder;
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return Database.GetAll().Where(x => x.Value.CustomerId == customerId).Select(x => x.Value).ToList();
        }
    }
}
