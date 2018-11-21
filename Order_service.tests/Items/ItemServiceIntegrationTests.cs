using System;
using System.Linq;
using Order_domain.Items;
using Order_domain.Items.Prices;
using Order_domain.tests.Items;
using Order_service.Items;
using Xunit;

namespace Order_service.tests.Items
{
    public class ItemServiceIntegrationTests : IDisposable
    {
        private readonly ItemService _itemService;
        private readonly ItemRepository _itemRepository;

        public ItemServiceIntegrationTests()
        {
            _itemRepository = new ItemRepository(new ItemDatabase());
            _itemService = new ItemService(_itemRepository, new ItemValidator());
        }

        public void Dispose()
        {
            _itemRepository.Reset();
        }

        [Fact]
        public void CreateItem()
        {
            Item createdItem = _itemService.CreateItem(ItemTestBuilder.AnItem()
                .WithName("The Martian")
                .WithDescription("A cool book written by a software engineer")
                .WithAmountOfStock(239)
                .WithPrice(Price.Create(new decimal(10.90)))
                .Build());

            Assert.NotNull(createdItem);
            Assert.NotEqual(Guid.Empty, createdItem.Id);
            Assert.Equal("The Martian", createdItem.Name);
            Assert.Equal("A cool book written by a software engineer", createdItem.Description);
            Assert.Equal(239, createdItem.AmountOfStock);
            Assert.Equal(new decimal(10.90), createdItem.Price.Amount);
        }

        [Fact]
        public void GetItem()
        {
            Item createdItem = _itemService.CreateItem(ItemTestBuilder.AnItem().Build());

            Item itemFromDb = _itemService.GetItem(createdItem.Id);

            Assert.NotNull(itemFromDb);
            Assert.Equal(createdItem, itemFromDb);
        }

        [Fact]
        public void GetAllItems()
        {
            Item createdItem1 = _itemService.CreateItem(ItemTestBuilder.AnItem().Build());
            Item createdItem2 = _itemService.CreateItem(ItemTestBuilder.AnItem().Build());

            var allItems = _itemService.GetAllItems().ToList();

            Assert.Contains(createdItem1, allItems);
            Assert.Contains(createdItem2, allItems);
        }
    }
}
