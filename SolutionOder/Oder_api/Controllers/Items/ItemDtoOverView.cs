using System;
using Order.Domain.Items;

namespace Order.Api.Controllers.Items
{
    public class ItemDtoOverView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
        public int ToOrder { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime LastDateOrder { get; set; }
        public DateTime LastDateSold { get; set; }
    }
}
