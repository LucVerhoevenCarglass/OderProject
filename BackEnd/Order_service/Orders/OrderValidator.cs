using System.Linq;
using Order_domain;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;

namespace Order_service.Orders
{
    public class OrderValidator : EntityValidator<Order>
    {
        protected override bool IsAFieldEmptyOrNull(Order order)
        {
            return IsNull(order)
                   || IsNull(order.CustomerId)
                   || !order.OrderItems.Any()
                   || order.OrderItems.All(IsOrderItemInvalid);
        }

        private bool IsOrderItemInvalid(OrderItem orderItem)
        {
            return IsNull(orderItem.ItemId) ||
                   IsNull(orderItem.ItemPrice) ||
                   IsNull(orderItem.ShippingDate) ||
                   orderItem.ItemPrice.GetAmountAsFloat() <= 0 ||
                   orderItem.OrderedAmount <= 0;
        }
    }
}
