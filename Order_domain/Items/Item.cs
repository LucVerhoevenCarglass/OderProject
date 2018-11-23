using System;
using Oder_infrastructure.builders;
using Order_domain.Items.Prices;

namespace Order_domain.Items
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Price Price { get; set; }
        public int AmountOfStock { get; set; }

        private Item():base(Guid.Empty) {}

        public Item(ItemBuilder itemBuilder)
            : base(itemBuilder.Id)
        {
            Name = itemBuilder.Name;
            Description = itemBuilder.Description;
            Price = itemBuilder.Price;
            AmountOfStock = itemBuilder.AmountOfStock;
        }

        public StockUrgency GetStockUrgency()
        {
            if (AmountOfStock < 5) { return StockUrgency.STOCK_LOW; }
            if (AmountOfStock < 10) { return StockUrgency.STOCK_MEDIUM; }
            return StockUrgency.STOCK_HIGH;
        }

        public override string ToString()
        {
            return "Item{" +
                    "id=" + Id + '\'' +
                    ", name='" + Name + '\'' +
                    ", description='" + Description + '\'' +
                    ", price=" + Price +
                    ", amountOfStock=" + AmountOfStock +
                    '}';
        }

        public void DecrementStock(int amountToDecrement)
        {
            if (amountToDecrement > AmountOfStock)
            {
                //IllegalArgumentException
                throw new Exception("Decrementing the stock amount of an item " + Id.ToString("N")
                        + " below 0 is not allowed");
            }
            AmountOfStock -= amountToDecrement;
        }

        public class ItemBuilder : Builder<Item>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Price Price { get; set; }
            public int AmountOfStock { get; set; }

            public static ItemBuilder Item()
            {
                return new ItemBuilder();
            }
            
            public override Item Build()
            {
                return new Item(this);
            }

            public ItemBuilder WithId(Guid id)
            {
                Id = id;
                return this;
            }

            public ItemBuilder WithName(string name)
            {
                Name = name;
                return this;
            }

            public ItemBuilder WithDescription(string description)
            {
                Description = description;
                return this;
            }

            public ItemBuilder WithPrice(Price price)
            {
                Price = price;
                return this;
            }

            public ItemBuilder WithAmountOfStock(int amountOfStock)
            {
                AmountOfStock = amountOfStock;
                return this;
            }
        }

        public enum StockUrgency
        {
            STOCK_LOW,
            STOCK_MEDIUM,
            STOCK_HIGH
        }
    }
}
