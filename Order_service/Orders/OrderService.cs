using System;
using System.Collections.Generic;
using System.Linq;
using Oder_infrastructure.Exceptions;
using Order_domain.Customers;
using Order_domain.Items;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;

namespace Order_service.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderValidator _orderValidator;

        public OrderService(ICustomerRepository customerRepository,
                        IItemRepository itemRepository,
                        IOrderRepository orderRepository,
                        OrderValidator orderValidator)
        {
            _customerRepository = customerRepository;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _orderValidator = orderValidator;
        }

        public Order CreateOrder(Order order)
        {
            AssertOrderIsValidForCreation(order);
            AssertOrderingCustomerExists(order);
            AssertAllOrderedItemsExist(order);
            return _orderRepository.Save(order);
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return _orderRepository.GetOrdersForCustomer(customerId);
        }

        public Order ReorderOrder(Guid orderId)
        {
            Order orderToReorder = _orderRepository.Get(orderId);
            AssertCustomerIsOwnerOfOrderToReorder(orderId, orderToReorder);
            return _orderRepository.Save(Order.OrderBuilder.Order()
                    .WithCustomerId(orderToReorder.CustomerId)
                    .WithOrderItems(CopyOrderItemsWithRecentPrice(orderToReorder.OrderItems))
                    .Build());
        }

        public IEnumerable<Order> GetAllOrders(bool onlyIncludeShippableToday)
        {
            if (onlyIncludeShippableToday)
            {
                return GetOrdersOnlyContainingOrderItemsShippingToday();
            }

            return _orderRepository.GetAll().Select(order => order.Value);
        }

        private IEnumerable<Order> GetOrdersOnlyContainingOrderItemsShippingToday()
        {
            return _orderRepository.GetAll()
                                   .Select(x => x.Value)
                                   .Select(order => Order.OrderBuilder.Order().WithCustomerId(order.CustomerId)
                                                                          .WithId(order.Id)
                                                                          .WithOrderItems(GetOrderItemsShippingToday(order)).Build());
        }

        private IEnumerable<OrderItem> GetOrderItemsShippingToday(Order order)
        {
            return order.OrderItems.Where(orderItem => orderItem.ShippingDate == DateTime.Now);
        }

        private void AssertAllOrderedItemsExist(Order order)
        {
            if (!DoAllOrderItemsReferenceAnExistingItem(order.OrderItems))
            {
                throw new EntityNotValidException("creation of a new order when checking if all the ordered items exist", order);
            }
        }

        private bool DoAllOrderItemsReferenceAnExistingItem(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.All(orderItem => _itemRepository.Get(orderItem.ItemId) != null);
        }

        private void AssertOrderingCustomerExists(Order order)
        {
            if (!DoesCustomerExist(order))
            {
                throw new EntityNotFoundException("creation of a new order when checking if the referenced customer exists", nameof(Customer), order.CustomerId);
            }
        }

        private bool DoesCustomerExist(Order order)
        {
            return _customerRepository.Get(order.CustomerId) != null;
        }

        private void AssertOrderIsValidForCreation(Order order)
        {
            if (!_orderValidator.IsValidForCreation(order))
            {
                _orderValidator.ThrowInvalidOperationException(order, "creation");
            }
        }

        private IEnumerable<OrderItem> CopyOrderItemsWithRecentPrice(IEnumerable<OrderItem> orderItems)
        {
            return orderItems.Select(orderItem =>
            {
                Item item = _itemRepository.Get(orderItem.ItemId);
                return OrderItem.OrderItemBuilder.OrderItem()
                    .WithItemId(orderItem.ItemId)
                    .WithOrderedAmount(orderItem.OrderedAmount)
                    .WithShippingDateBasedOnAvailableItemStock(item.AmountOfStock)
                    .WithItemPrice(item.Price)
                    .Build();
            });
        }

        private void AssertCustomerIsOwnerOfOrderToReorder(Guid orderId, Order orderToReorder)
        {
            if (!DoesOrderToReorderBelongToAuthenticatedUser(orderToReorder.CustomerId))
            {
                throw new NotAuthorizedException("Customer " + orderToReorder.CustomerId.ToString("N") + " is not allowed " + "to reorder the Order " + orderId.ToString("N") + " because he's not the owner of that order!");
            }
        }

        private bool DoesOrderToReorderBelongToAuthenticatedUser(Guid customerId)
        {
            return _customerRepository.Get(customerId) != null;
        }
    }
}
