using System.Collections.Generic;
using Order.Domain.Orders;
using Order.Services.Orders;

namespace Order.Api.Controllers.Orders
{
    public class OrderDtoDetail
    {
        public string OrderId { get; set; }
        public List<ItemGroups> ItemGroups { get; set; }
        public decimal TotalPrice  { get; set; }

    }
}
