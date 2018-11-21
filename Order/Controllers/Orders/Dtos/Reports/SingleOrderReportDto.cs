using System;
using System.Collections.Generic;

namespace Order_api.Controllers.Orders.Dtos.Reports
{
    public class SingleOrderReportDto
    {
        public string OrderId { get; set; }
        public IEnumerable<ItemGroupReportDto> ItemGroups { get; set; }

        public SingleOrderReportDto WithItemGroups(params ItemGroupReportDto[] itemGroups)
        {                           
            ItemGroups = itemGroups;
            return this;            
        }                           
                                    
        public SingleOrderReportDto WithOrderId(string orderId)
        {                           
            OrderId = orderId;
            return this;
        }
    }
}
