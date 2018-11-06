using System.Collections.Generic;
using Order.Domain.Orders;

namespace Order.Services.Orders
{
    public interface IOrderService
    {
        IEnumerable<MainOrder> GetAllOrders();
        string CreateNewOrder(MainOrder mainOrder);
        IEnumerable<MainOrder> GetOrdersForCustomer(string customerId);
    }
}
