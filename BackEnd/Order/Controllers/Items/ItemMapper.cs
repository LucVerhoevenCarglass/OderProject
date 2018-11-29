using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oder_infrastructure.dto;
using Order_domain.Items;
using Order_domain.Items.Prices;

namespace Order_api.Controllers.Items
{
    public class ItemMapper : Mapper<ItemDto, Item>
    {
        public Item ToDomain(Guid itemId, ItemDto itemDto)
        {
            if (string.IsNullOrWhiteSpace(itemDto.Id))
            {
                return ToDomain(itemDto.WithId(itemId));
            }

            if (itemId != new Guid(itemDto.Id))
            {
                throw new ArgumentException(
                    "When updating an item, the provided ID in the path should match the ID in the body: " +
                    "ID in path = " + itemId.ToString("N") + ", ID in body = " + itemDto.Id);
            }

            return ToDomain(itemDto);
        }

        public override Item ToDomain(ItemDto itemDto)
        {
            return Item.ItemBuilder.Item()
                .WithId(string.IsNullOrWhiteSpace(itemDto.Id) ? Guid.Empty : new Guid(itemDto.Id))
                .WithName(itemDto.Name)
                .WithDescription(itemDto.Description)
                .WithAmountOfStock(itemDto.AmountOfStock)
                .WithPrice(Price.Create(new decimal(itemDto.Price)))
                .Build();
        }

        public override ItemDto ToDto(Item item)
        {
            return new ItemDto()
                .WithId(item.Id)
                .WithName(item.Name)
                .WithDescription(item.Description)
                .WithAmountOfStock(item.AmountOfStock)
                .WithPrice(item.Price.GetAmountAsFloat())
                .WithStockUrgency(item.GetStockUrgency().ToString());
        }
    }
}