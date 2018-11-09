using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Order.Databases;
using Order.Domain;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Services.Items;

namespace Order.Services.Orders
{
    public class OrderService: IOrderService
    {
        private const string ErrorMessage = "OrderService : ";
        private readonly ILogger<OrderService> _logger;
        private readonly IItemService _itemService;
        private readonly IOrdersDatabase _ordersDatabase;
        public OrderService(ILogger<OrderService> logger, IItemService itemservice, IOrdersDatabase ordersDatabase)
        {
            _logger = logger;
            _itemService = itemservice;
            _ordersDatabase = ordersDatabase;
        }

        public IEnumerable<MainOrder> GetAllOrders()
        {
            return _ordersDatabase.GetDatabase();
        }

        public string CreateNewOrder(MainOrder mainOrder)
        {
            _ordersDatabase.AddOrder(mainOrder);
            mainOrder.ItemGroups.ForEach(item => _itemService.ChangeStock(item.ItemId, item.OrderAmount));                   
            return $"Total to pay : {mainOrder.CalculateTotalToPay().ToString(CultureInfo.InvariantCulture)}";
        }

        public IEnumerable<MainOrder> GetOrdersForCustomer(string customerId)
        {
            var ordersForCustomer = GetAllOrders().Where(order => order.CustomerId == customerId).ToList();
            if (!ordersForCustomer.Any())
            {
                throw new OrderExeptions($"{ErrorMessage} No Orders Found for CustomerId {customerId}");
            }
            return ordersForCustomer;
        }
    }
}
