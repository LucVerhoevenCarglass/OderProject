using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Items;

namespace Order.Databases
{
    public static class ItemsDatabase
    {
        public static List<Item> Items = new List<Item>();

        public static void InitDatabase()
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
    }
}
