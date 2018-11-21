using System;
using System.Collections.Generic;
using System.Linq;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Orders.Dtos;
using Order_api.Controllers.Orders.Dtos.Reports;
using Order_domain.Customers.Addresses;
using Order_domain.Orders;
using Order_service.Customers;

namespace Order_api.Controllers.Orders
{
    public class OrderMapper
    {
        private readonly OrderItemMapper _orderItemMapper;
        private readonly AddressMapper _addressMapper;
        private readonly ICustomerService _customerService;

        public OrderMapper(OrderItemMapper orderItemMapper, AddressMapper addressMapper, ICustomerService customerService)
        {
            _orderItemMapper = orderItemMapper;
            _addressMapper = addressMapper;
            _customerService = customerService;
        }

        public OrderDto ToDto(Order order)
        {
            return new OrderDto()
                .WithOrderId(order.Id.ToString("N"))
                .WithAddress(_addressMapper.ToDto(GetAddressForCustomer(order.CustomerId)))
                .WithItemGroups(order.OrderItems.Select(item => _orderItemMapper.ToDto(item)).ToArray());
        }

        private Address GetAddressForCustomer(Guid customerId)
        {
            return _customerService.GetCustomer(customerId).Address;
        }

        public Order ToDomain(OrderCreationDto orderCreationDto)
        {
            return Order.OrderBuilder.Order()
                    .WithCustomerId(new Guid(orderCreationDto.CustomerId))
                    .WithOrderItems(orderCreationDto.ItemGroups.Select(group => _orderItemMapper.ToDomain(group)))
                    .Build();
        }

        public OrderAfterCreationDto ToOrderAfterCreationDto(Order order)
        {
            return new OrderAfterCreationDto()
                    .WithOrderId(order.Id.ToString())
                    .WithTotalPrice(order.GetTotalPrice().GetAmountAsFloat());
        }

        public OrdersReportDto ToOrdersReportDto(List<Order> orders)
        {
            return new OrdersReportDto()
                    .WithOrders(orders.Select(ToSingleOrderReportDto).ToArray())
                    .WithTotalPriceOfAllOrders(orders.Sum(order => order.GetTotalPrice().GetAmountAsFloat()));
        }

        private SingleOrderReportDto ToSingleOrderReportDto(Order order)
        {
            return new SingleOrderReportDto()
                    .WithOrderId(order.Id.ToString())
                    .WithItemGroups(order.OrderItems.Select(item => _orderItemMapper.ToItemGroupReportDto(item)).ToArray());
        }
    }
}
