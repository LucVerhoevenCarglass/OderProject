using System;
using Oder_infrastructure.dto;
using Oder_infrastructure.Exceptions;
using Order_api.Controllers.Orders.Dtos;
using Order_api.Controllers.Orders.Dtos.Reports;
using Order_domain.Items;
using Order_domain.Items.Prices;
using Order_domain.Orders.OrderItems;
using Order_service.Items;

namespace Order_api.Controllers.Orders
{
    public class OrderItemMapper : Mapper<ItemGroupDto, OrderItem>
    {
        private readonly IItemService _itemService;

        public OrderItemMapper(IItemService itemService)
        {
            _itemService = itemService;
        }

        public override OrderItem ToDomain(ItemGroupDto itemGroupDto)
        {
            return OrderItem.OrderItemBuilder.OrderItem()
                .WithItemId(new Guid(itemGroupDto.ItemId))
                .WithOrderedAmount(itemGroupDto.OrderedAmount)
                .WithItemPrice(EnrichWithItemPrice(itemGroupDto))
                .WithShippingDateBasedOnAvailableItemStock(EnrichWithItemAmountOfStock(itemGroupDto))
                .Build();
        }

        public override ItemGroupDto ToDto(OrderItem orderItem)
        {
            return new ItemGroupDto()
                .WithItemId(orderItem.ItemId.ToString())
                .WithOrderedAmount(orderItem.OrderedAmount);
        }

        public ItemGroupReportDto ToItemGroupReportDto(OrderItem orderItem)
        {
            return new ItemGroupReportDto()
                .WithItemId(orderItem.ItemId.ToString())
                .WithOrderedAmount(orderItem.OrderedAmount)
                .WithName(EnrichWithItemName(orderItem))
                .WithTotalPrice(orderItem.GetTotalPrice().GetAmountAsFloat());
        }

        private Price EnrichWithItemPrice(ItemGroupDto itemGroupDto)
        {
            return GetItemForId(itemGroupDto.ItemId).Price;
        }

        private int EnrichWithItemAmountOfStock(ItemGroupDto itemGroupDto)
        {
            return GetItemForId(itemGroupDto.ItemId).AmountOfStock;
        }

        private string EnrichWithItemName(OrderItem orderItem)
        {
            return GetItemForId(orderItem.ItemId.ToString()).Name;
        }

        private Item GetItemForId(string itemIdAsString)
        {
            Item item = _itemService.GetItem(new Guid(itemIdAsString));
            if (item == null)
            {
                throw new EntityNotFoundException("mapping to an order of an item group (for creating a new order)", nameof(item), new Guid(itemIdAsString));
            }

            return item;
        }
    }
}