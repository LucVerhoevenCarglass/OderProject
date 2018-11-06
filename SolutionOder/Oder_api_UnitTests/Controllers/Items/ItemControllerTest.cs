using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Order.Api.Controllers.Items;
using Order.Domain;
using Order.Domain.Items;
using Order.Services.Items;
using Xunit;
using Xunit.Abstractions;

namespace Order.Api.UnitTests.Controllers.Items
{

    public class ItemControllerTest
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService itemService;
        private readonly IItemMapper itemMapper;
        private readonly ItemController itemController;

        public ItemControllerTest()
        {
            _logger = Substitute.For<ILogger<ItemController>>();
             itemService = Substitute.For<IItemService>();
             itemMapper = Substitute.For<IItemMapper>();
             itemController = new ItemController(itemService, itemMapper, _logger);
        }

        [Fact]
        public void GivenItemController_WhenCreateNewItem_ThenReturnOk()
        {
            //Given
            ItemDtoToCreate newItemDtoToCreate= new ItemDtoToCreate()
            {
                    Name="ProductId",
                    Description="Description",
                    Price = 10,
                    StockAmount=2
            };
            Item newItem = new Item();
            itemMapper.FromCreateItemDto_To_Item(newItemDtoToCreate).Returns(newItem);
            itemService.CreateNewItem(newItem).Returns(newItem);

            //when
            var checkOk = itemController.CreateNewItem(newItemDtoToCreate).Result;

            //then
            Assert.IsType<OkObjectResult>(checkOk);
        }

        [Fact]
        public void GivenItemController_WhenCreateNewItemWithBadInput_ThenReturnBadRequest()
        {
            //Given
            ItemDtoToCreate newItemDtoToCreate = new ItemDtoToCreate();
            Item newItem = new Item();

            //Action act = () => itemMapper.FromCreateItemDto_To_Item(newItemDtoToCreate);
            //Exception thrownException = Assert.Throws<Exception>(act);
            //Assert.Contains("is required", thrownException.Message);

            itemMapper.FromCreateItemDto_To_Item(newItemDtoToCreate).Returns(x =>
            {
                throw new OrderExeptions("Name required");
            });

            //when
            var checkOk = itemController.CreateNewItem(newItemDtoToCreate).Result;

            //then
            Assert.IsType<BadRequestObjectResult>(checkOk);
        }

    }
}
