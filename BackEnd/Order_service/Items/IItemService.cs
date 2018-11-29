using System;
using System.Collections.Generic;
using Order_domain.Items;

namespace Order_service.Items
{
    public interface IItemService
    {
        Item CreateItem(Item item);

        Item UpdateItem(Item item);

        Item GetItem(Guid itemId);

        void DecrementStockForItem(Guid itemId, int amountToDecrement);

        IEnumerable<Item> GetAllItems();
    }
}