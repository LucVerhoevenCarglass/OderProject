using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Items;

namespace Order.Domain.Orders
{
    public class ItemGroups
    {
        private const string ErrorMessage = "ItemGroups : ";
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int OrderAmount { get; set; }
        public int BackOrder { get; set; }
        public DateTime OrderShippingDate { get; set; }

        public void SetShippingDate(Item findItem)
        {
            if (findItem.StockAmount >= OrderAmount)
            {
                OrderShippingDate = DateTime.Today.AddDays(1);
            }
            else
            {
                OrderShippingDate = DateTime.Today.AddDays(7);
            }
        }

        public int SetOrderAmount(Item findItem)
        {
            int deleveryAmount = Math.Min(findItem.StockAmount, OrderAmount);
            if (findItem.Status == ItemStatus.SellOut)
            {
                OrderAmount = deleveryAmount;
                return 0;
            }

            BackOrder = OrderAmount - deleveryAmount;
            return BackOrder;

        }
    }
}
