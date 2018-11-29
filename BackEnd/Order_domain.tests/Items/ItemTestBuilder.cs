using System;
using Oder_infrastructure.builders;
using Order_domain.Items;
using Order_domain.Items.Prices;

namespace Order_domain.tests.Items
{
    public class ItemTestBuilder : Builder<Item>
    {
        private readonly Item.ItemBuilder _itemBuilder;

        private ItemTestBuilder(Item.ItemBuilder itemBuilder)
        {
            _itemBuilder = itemBuilder;
        }

        public static ItemTestBuilder AnEmptyItem()
        {
            
            return new ItemTestBuilder(Item.ItemBuilder.Item());
        }

        public static ItemTestBuilder AnItem()
        {
            return new ItemTestBuilder(Item.ItemBuilder.Item()
                .WithName("Headphone")
                .WithDescription("Just a simple headphone")
                .WithAmountOfStock(50)
                .WithPrice(Price.Create(new decimal(49.95))));
        }

        public override Item Build()
        {
            return _itemBuilder.Build();
        }

        public ItemTestBuilder WithId(Guid id)
        {
            _itemBuilder.WithId(id);
            return this;
        }

        public ItemTestBuilder WithName(string name)
        {
            _itemBuilder.WithName(name);
            return this;
        }

        public ItemTestBuilder WithDescription(string description)
        {
            _itemBuilder.WithDescription(description);
            return this;
        }

        public ItemTestBuilder WithPrice(Price price)
        {
            _itemBuilder.WithPrice(price);
            return this;
        }

        public ItemTestBuilder WithAmountOfStock(int amountOfStock)
        {
            _itemBuilder.WithAmountOfStock(amountOfStock);
            return this;
        }
    }
}