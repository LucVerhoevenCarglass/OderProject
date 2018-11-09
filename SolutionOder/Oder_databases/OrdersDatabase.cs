using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Order.Domain.Orders;

namespace Order.Databases
{
    public class OrdersDatabase: IOrdersDatabase
    {
        private readonly List<MainOrder> Orders = new List<MainOrder>();
        private const string ErrorMessage = "OrderDatabase : ";
        private readonly ILogger<OrdersDatabase> _logger;

        public OrdersDatabase(ILogger<OrdersDatabase> logger)
        {
            _logger = logger;
        }

        public void AddOrder(MainOrder newOrder)
        {
            Orders.Add(newOrder);
        }

        public List<MainOrder> GetDatabase()
        {
            return Orders;
        }

    }
}
