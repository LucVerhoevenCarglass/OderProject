using System.Collections.Generic;

namespace Order_api.Controllers.Orders.Dtos.Reports
{
    public class OrdersReportDto
    {
        public float TotalPriceOfAllOrders { get; set; }
        public IEnumerable<SingleOrderReportDto> Orders { get; set; }

        public OrdersReportDto WithTotalPriceOfAllOrders(float totalPriceOfAllOrders)
        {
            TotalPriceOfAllOrders = totalPriceOfAllOrders;
            return this;
        }

        public OrdersReportDto WithOrders(params SingleOrderReportDto[] orders)
        {
            Orders = orders;
            return this;
        }
    }
}
