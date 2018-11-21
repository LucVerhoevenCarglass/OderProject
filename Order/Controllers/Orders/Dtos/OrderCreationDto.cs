using System.Collections.Generic;

namespace Order_api.Controllers.Orders.Dtos
{
    public class OrderCreationDto
    {
        public string CustomerId { get; set; }
        public IEnumerable<ItemGroupDto> ItemGroups { get; set; }

        public OrderCreationDto WithItemGroups(params ItemGroupDto[] itemGroups)
        {
            ItemGroups = itemGroups;
            return this;
        }

        public OrderCreationDto WithCustomerId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }
    }
}