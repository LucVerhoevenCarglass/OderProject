using System;

namespace Order_api.Controllers.Items
{
    public class ItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int AmountOfStock { get; set; }
        public string StockUrgency { get; set; }

        public ItemDto WithId(Guid id)
        {
            Id = id.ToString("N");
            return this;
        }

        public ItemDto WithoutId()
        {
            Id = null;
            return this;
        }

        public ItemDto WithName(string name)
        {
            Name = name;
            return this;
        }

        public ItemDto WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public ItemDto WithPrice(float price)
        {
            Price = price;
            return this;
        }

        public ItemDto WithAmountOfStock(int amountOfStock)
        {
            AmountOfStock = amountOfStock;
            return this;
        }

        public ItemDto WithStockUrgency(string stockUrgency)
        {
            StockUrgency = stockUrgency;
            return this;
        }
    }
}
