using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Items;

namespace Order.Services.Items
{
    public interface IItemService
    {
        IEnumerable<Item> GetAllItems();
        IEnumerable<Item> GetForSalesItems();
        Item CreateNewItem(Item itemToCreate);
        Item GetForSalesItemByName(string itemName);
        void ChangeStock(string itemId, int orderAmount);
        void SetToOrder(Item itemSetToOrder, int amountBackOrder);
    }
}
