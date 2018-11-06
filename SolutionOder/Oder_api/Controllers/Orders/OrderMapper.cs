using System.Collections.Generic;
using System.Linq;
using Order.Domain;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Services.Customers;
using Order.Services.Items;
using Order.Services.Orders;

namespace Order.Api.Controllers.Orders
{
    public class OrderMapper: IOrderMapper
    {
        private readonly IItemService _itemService;
        private readonly ICustomerService _customerService;

        public OrderMapper(IItemService itemService, ICustomerService customerService)
        {
            _itemService = itemService;
            _customerService = customerService;
        }

        public OrderDtoDetail FromMainOrder_To_OrderDetail(MainOrder mainOrder)
        {
            return new OrderDtoDetail()
            {
                OrderId = mainOrder.OrderId,
                ItemGroups = mainOrder.ItemGroups,
                TotalPrice = mainOrder.CalculateTotalToPay()
        };
        }

        public MainOrder FromItemDtoToOrder_To_MainOrder(string customerId, List<ItemDtoToOrder> itemDtoToOrder)
        {
            _customerService.GetDetailCustomer(customerId);
            MainOrder newOrder = new MainOrder(customerId: customerId)
            {
                ItemGroups = itemDtoToOrder.Select(item => CreateItemGroup(item)).ToList()
            };
            return newOrder;
        }

        private ItemGroups CreateItemGroup(ItemDtoToOrder itemDtoOrder)
        {
            if (itemDtoOrder.OrderAmount <= 0)
                throw new OrderExeptions($"OrderMapper: OrderAmount should be > 0 ");

            Item findItem = _itemService.GetForSalesItemByName(itemDtoOrder.Name);
            ItemGroups itemGroups = new ItemGroups()
            {
                Name = itemDtoOrder.Name,
                OrderAmount = itemDtoOrder.OrderAmount,
                ItemId = findItem.ItemId,
                Description = findItem.Description,
                Price = findItem.Price,
            };
            int amountBackOrder=itemGroups.SetOrderAmount(findItem);
            itemGroups.SetShippingDate(findItem);
            if (amountBackOrder > 0)
            {
                _itemService.SetToOrder(findItem, amountBackOrder);
            }
            return itemGroups;
        }
    }
}
