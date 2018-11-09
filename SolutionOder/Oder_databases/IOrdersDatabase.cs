using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Orders;

namespace Order.Databases
{
    public interface IOrdersDatabase
    {
        void AddOrder(MainOrder newOrder);
        List<MainOrder> GetDatabase();
    }
}
