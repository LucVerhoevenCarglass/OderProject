using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Order.Api.Controllers.Items;
using Order.Api.Controllers.Orders;
using Order.Domain;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Services.Items;
using Order.Services.Orders;
using Xunit;
using Xunit.Abstractions;

namespace Order.Api.UnitTests.Controllers.Orders
{

    public class OrderControllerTest
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService orderService;
        private readonly IOrderMapper orderMapper;
        private readonly OrderController orderController;

        public OrderControllerTest()
        {
            _logger = Substitute.For<ILogger<OrderController>>();
            orderService = Substitute.For<IOrderService>();
            orderMapper = Substitute.For<IOrderMapper>();
            orderController = new OrderController(orderService, orderMapper, _logger);
        }

        [Fact]
        public void GivenOrderController_WhenCreateNewValidOrder_ThenReturnOk()
        {
            //Given
            List<ItemDtoToOrder> itemsDtoToOrder = new List<ItemDtoToOrder>();
            MainOrder newOrder= new MainOrder("CustomerId");
            orderMapper.FromItemDtoToOrder_To_MainOrder("CustomerId", itemsDtoToOrder).Returns(newOrder);
            orderService.CreateNewOrder(newOrder).Returns("500");

            //when
            var checkOk = orderController.OrderItems("CustomerId", itemsDtoToOrder).Result;

            //then
            Assert.IsType<OkObjectResult>(checkOk);
        }

        [Fact]
        public void GivenOrderController_WhenCreateNewOrderThatThrowAnExeption_ThenReturnBadRequest()
        {
            //Given
            List<ItemDtoToOrder> itemsDtoToOrder = new List<ItemDtoToOrder>();
            MainOrder newOrder = new MainOrder("CustomerId");
            orderMapper.FromItemDtoToOrder_To_MainOrder("CustomerId", itemsDtoToOrder)
                    .Returns(x => throw new OrderExeptions("Some kind of Error"));


            //when
            var checkOk = orderController.OrderItems("CustomerId", itemsDtoToOrder).Result;

            //then
            Assert.IsType<BadRequestObjectResult>(checkOk);
        }


    }
}
