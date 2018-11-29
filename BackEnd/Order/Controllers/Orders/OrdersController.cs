using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Order_api.Controllers.Orders.Dtos;
using Order_api.Controllers.Orders.Dtos.Reports;
using Order_service.Orders;

namespace Order_api.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly OrderMapper _orderMapper;

        public OrdersController(IOrderService orderService, OrderMapper orderMapper)
        {
            _orderService = orderService;
            _orderMapper = orderMapper;
        }

        [HttpGet]
        public List<OrderDto> GetAllOrders([FromQuery] bool shippableToday)
        {
            return _orderService.GetAllOrders(shippableToday)
                .Select(order => _orderMapper.ToDto(order))
                .ToList();
        }

        [HttpPost]
        public OrderAfterCreationDto CreateOrder([FromBody] OrderCreationDto orderDto)
        {
            return _orderMapper.ToOrderAfterCreationDto(
                _orderService.CreateOrder(
                    _orderMapper.ToDomain(orderDto)));
        }

        [HttpPost("/{id}/reorder")]
        public OrderAfterCreationDto ReorderOrder([FromRoute] string id)
        {
            return _orderMapper.ToOrderAfterCreationDto(
                _orderService.ReorderOrder(new Guid(id)));
        }

        [HttpGet("/customers/{customerId}")]
        public OrdersReportDto GetOrdersForCustomerReport([FromRoute] string customerId)
        {
            return _orderMapper.ToOrdersReportDto(
                _orderService.GetOrdersForCustomer(new Guid(customerId)).ToList());
        }
    }
}
