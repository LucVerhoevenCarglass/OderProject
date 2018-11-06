using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Order.Domain.Orders
{
    public class MainOrder
    {
        public string CustomerId { get; set; }
        public string OrderId { get; }
        public List<ItemGroups> ItemGroups { get; set; }


        public MainOrder(string customerId)
        {
            CustomerId = customerId;
            OrderId = Guid.NewGuid().ToString();
        }

        public decimal CalculateTotalToPay()
        {
            return ItemGroups.Sum(item => item.OrderAmount * item.Price);
        }
    }
}
