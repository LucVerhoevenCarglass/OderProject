using System;
using System.Collections.Generic;
using System.Linq;
using Order_domain.Orders.OrderItems;
using Order_service.Data;

namespace Order_domain.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        //private ApplicationEventPublisher eventPublisher;
        private readonly OrderContext _context;


        public OrderRepository( OrderContext context)
            : base(context)
        {
            _context = context;
        }

        public Order Save(Order entity)
        {
            Order savedOrder = base.Save(entity);
            //TODO check if ID not exist ?
            entity.GenerateId();
            _context.Orders.Add(entity);         
            foreach (OrderItem entityOrderItem in entity.OrderItems)
            {
                entityOrderItem.OrderId = entity.Id;
                _context.OrderItems.Add(entityOrderItem);
            }
            return entity;
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return _context.Orders.Where(order => order.CustomerId == customerId).ToList();

        }
    }
}
