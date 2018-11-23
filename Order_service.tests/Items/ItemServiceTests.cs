using System;
using NSubstitute;
using Order_domain.Customers;
using Order_domain.Items;
using Order_domain.tests.Items;
using Order_service.Customers;
using Order_service.Data;
using Order_service.Items;
using Xunit;

namespace Order_service.tests.Items
{
    public class ItemServiceTests
    {
        private readonly ItemService _itemService;
        private readonly IItemRepository _itemRepository;

        public ItemServiceTests()
        {
            _itemRepository = Substitute.For<IItemRepository>();
            _itemService = new ItemService(_itemRepository, new ItemValidator());
        }
    
        [Fact]
        public void createItem_happyPath()
        {
            Item item = ItemTestBuilder.AnItem().Build();

            _itemRepository.Save(item).Returns(item);
            Item createdItem = _itemService.CreateItem(item);

            Assert.NotNull(createdItem);
        }

        [Fact]
        public void createItem_givenItemThatIsNotValidForCreation_thenThrowException()
        {
            Item item = ItemTestBuilder.AnItem()
                .WithName(string.Empty)
                .Build();

            Exception ex = Assert.Throws<InvalidOperationException>(() => _itemService.CreateItem(item));
            Assert.Contains("Invalid Item provided for creation", ex.Message);
        }

        [Fact]
        public void updateItem_happyPath()
        {
            Item item = ItemTestBuilder.AnItem().WithId(Guid.NewGuid()).Build();

            _itemRepository.Update(item).Returns(item);
            Item updatedItem = _itemService.UpdateItem(item);

            Assert.NotNull(updatedItem);
        }

        [Fact]
        public void updateItem_givenItemThatIsNotValidForUpdating_thenThrowException()
        {
            Item item = ItemTestBuilder.AnItem()
                .WithAmountOfStock(0)
                .Build();

            Exception ex = Assert.Throws<InvalidOperationException>(() => _itemService.UpdateItem(item));
            Assert.Contains("Invalid Item provided for updating", ex.Message);
        }
    }
}
