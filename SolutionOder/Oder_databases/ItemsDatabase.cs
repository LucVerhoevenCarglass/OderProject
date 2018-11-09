using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Order.Domain.Items;

namespace Order.Databases
{
    public class ItemsDatabase: IItemsDatabase
    {
        private const string ErrorMessage = "ItemDatabase : ";
        private readonly List<Item> Items = new List<Item>();
        private readonly ILogger<ItemsDatabase> _logger;

        public ItemsDatabase(ILogger<ItemsDatabase> logger)
        {
            InitDatabase();
        }

        public List<Item> GetDatabase()
        {
            return Items;
        }

        public void InitDatabase()
        {
            Random stock = new Random();
            for (int itemCount = 1; itemCount < 100; itemCount++)
            {
                Items.Add(new Item()
                {
                    Name= $"Product{itemCount}",
                    Description= $"Description {itemCount}",
                    Price = stock.Next(0,1000),
                    StockAmount = stock.Next(0,30)
                });
            }

        }

        public void ClearDatabase()
        {
            Items.Clear();
        }

        public void AddItem(Item newItem)
        {
            Items.Add(newItem);
        }
    }
}
