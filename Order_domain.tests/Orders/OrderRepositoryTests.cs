using System;
using System.Collections.Generic;
using System.Linq;
using Order_domain.Orders;
using Xunit;

namespace Order_domain.tests.Orders
{
    public class OrderRepositoryTests
    {
        private readonly OrderRepository _orderRepository;
        //private readonly OrderDatabase _orderDatabase;

        //public OrderRepositoryTests()
        //{
        //    _orderDatabase = new OrderDatabase();
        //    _orderRepository = new OrderRepository(_orderDatabase);
        //}

        //[Fact]
        //public void GetOrdersForCustomer()
        //{
        //    Guid customerId = Guid.NewGuid();

        //    Order order1 = OrderTestBuilder.AnOrder().WithCustomerId(customerId).WithId(Guid.NewGuid()).Build();
        //    Order order2 = OrderTestBuilder.AnOrder().WithCustomerId(Guid.NewGuid()).WithId(Guid.NewGuid()).Build();
        //    Order order3 = OrderTestBuilder.AnOrder().WithCustomerId(customerId).WithId(Guid.NewGuid()).Build();
            
        //    _orderDatabase.Populate(order1, order2, order3);

        //    List<Order> ordersForCustomer = _orderRepository.GetOrdersForCustomer(customerId).ToList();

        //    Assert.Contains(order1, ordersForCustomer);
        //    Assert.Contains(order3, ordersForCustomer);
        //}
    }
}
