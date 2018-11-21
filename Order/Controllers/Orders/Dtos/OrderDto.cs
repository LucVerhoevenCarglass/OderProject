using System;
using System.Collections.Generic;
using Order_api.Controllers.Customers.Addresses;

namespace Order_api.Controllers.Orders.Dtos
{
    public class OrderDto
    {
        public string OrderId { get; set; }
        public IEnumerable<ItemGroupDto> ItemsGroups { get; set; }
        public AddressDto Address { get; set; }

        public OrderDto WithItemGroups(params ItemGroupDto[] itemGroups)
        {
            ItemsGroups = itemGroups;
            return this;
        }

        public OrderDto WithAddress(AddressDto address)
        {
            Address = address;
            return this;
        }

        public OrderDto WithOrderId(String orderId)
        {
            OrderId = orderId;
            return this;
        }
    }
}
