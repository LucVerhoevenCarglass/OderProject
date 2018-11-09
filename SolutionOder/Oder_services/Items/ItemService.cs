using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Order.Databases;
using Order.Domain;
using Order.Domain.Items;

namespace Order.Services.Items
{
    public class ItemService: IItemService
    {
        private const string ErrorMessage = "ItemService : ";
        private readonly ILogger<ItemService> _logger;
        private readonly IItemsDatabase _itemsDatabase;

        public ItemService(ILogger<ItemService> logger, IItemsDatabase itemsDatabase)
        {
            _logger = logger;
            _itemsDatabase = itemsDatabase;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemsDatabase.GetDatabase();

        }

        public IEnumerable<Item> GetForSalesItems()
        {
            return GetAllItems().Where(item => 
                item.Status == ItemStatus.Active 
                || (item.Status == ItemStatus.SellOut && item.StockAmount > 0));
        }

        public Item GetForSalesItemByName(string itemName)
        {
            Item searchItem = GetForSalesItems().ToList().Find(item => item.Name == itemName);
            if (searchItem==null)
            {
                throw new OrderExeptions($"{ErrorMessage} Item {itemName} does not exists");
            }
            return searchItem;
        }

        public void ChangeStock(string itemId, int orderAmount)
        {
            foreach (var item in GetAllItems().Where(item => item.ItemId==itemId))
            {
                item.StockAmount -= Math.Min(orderAmount, item.StockAmount);
                item.LastDateSold = DateTime.Today;
            }
        }

        public void SetToOrder(Item itemSetToOrder, int amountBackOrder)
        {
            itemSetToOrder.ToOrder += amountBackOrder;
        }

        public Item CreateNewItem(Item itemToCreate)
        {
            if (GetAllItems().Any(item => item.Name == itemToCreate.Name)) 
            {
                _logger.LogError($"{ErrorMessage} Item  {itemToCreate.Name} already exists ");
                throw new OrderExeptions($"{ErrorMessage} Item already exists");
            }
            _itemsDatabase.AddItem(itemToCreate);
            return itemToCreate;
        }
    }
}
