using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Api.Controllers.Items;
using Order.Domain;
using Order.Domain.Orders;
using Order.Services.Items;
using Order.Services.Orders;

namespace Order.Api.Controllers.Orders
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderMapper _orderMapper;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(    IOrderService orderService, IOrderMapper orderMapper,
                                   ILogger<OrderController> logger)
        {
            _orderMapper = orderMapper;
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost("{customerId}")]
        [Authorize(Policy = "Customer")]
        //public ActionResult<string> OrderItems([FromBody] JObject data)
        public ActionResult<string> OrderItems(string customerId, [FromBody] List<ItemDtoToOrder> itemDtoToOrder)

        {
            try
            {
                //string customerId = data["customerdata"].ToObject<string>();
                //List<ItemDtoToOrder> itemDtoToOrder = data["orderData"].ToObject<List<ItemDtoToOrder>>();
                string totalPrice = _orderService.CreateNewOrder(_orderMapper.FromItemDtoToOrder_To_MainOrder(customerId, itemDtoToOrder));
                return Ok(totalPrice);
            }
            catch (OrderExeptions itemEx)
            {
                return Logexeption(itemEx.Message);
            }
            catch (Exception ex)
            {
                return Logexeption(ex.Message);
            }
        }

        [HttpGet("{customerId}")]
        [Authorize(Policy = "Customer")]
        //public ActionResult<string> OrderItems([FromBody] JObject data)
        public ActionResult<List<MainOrder>> GetOrdersForCustomer(string customerId)
        {
            try
            {
                IEnumerable<MainOrder> orders = _orderService.GetOrdersForCustomer(customerId);
                return Ok(orders.Select(mnOrder => _orderMapper.FromMainOrder_To_OrderDetail(mnOrder)));

            }
            catch (OrderExeptions itemEx)
            {
                return Logexeption(itemEx.Message);
            }
            catch (Exception ex)
            {
                return Logexeption(ex.Message);
            }
        }

        private ActionResult Logexeption(string itemExMessage)
        {
            _logger.LogError(itemExMessage);
            return BadRequest(itemExMessage);
        }
    }
}