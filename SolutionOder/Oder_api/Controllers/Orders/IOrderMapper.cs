using System.Collections.Generic;
using Order.Domain.Orders;

namespace Order.Api.Controllers.Orders
{
    public interface IOrderMapper
    {
        MainOrder FromItemDtoToOrder_To_MainOrder( string customerId, List<ItemDtoToOrder> itemDtoToOrder);
        OrderDtoDetail FromMainOrder_To_OrderDetail(MainOrder mainOrder);
    }
}
