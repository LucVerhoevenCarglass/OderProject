using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Domain.Items;

namespace Order.Api.Controllers.Items
{
    public class ItemMapper: IItemMapper
    {
        public ItemDtoOverView ItemDtoOverView(Item item)
        {
            return new ItemDtoOverView
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                StockAmount = item.StockAmount,
                ToOrder=item.ToOrder,
                Status = item.Status,
                LastDateOrder = item.LastDateOrder,
                LastDateSold = item.LastDateSold
            };
        }

        public Item FromCreateItemDto_To_Item(ItemDtoToCreate itemToCreate)
        { 
                 Item newItem = new Item
                    { Name = itemToCreate.Name,
                      Description= itemToCreate.Description,
                      Price = itemToCreate.Price,
                      StockAmount = itemToCreate.StockAmount
                 };
                newItem.CheckItemValues();
                return newItem;
        }

        public ItemDtoToCreate FromItem_To_ItemDtoToCreate(Item item)
        {
            return new ItemDtoToCreate
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                StockAmount = item.StockAmount
            };
        }
    }
}
