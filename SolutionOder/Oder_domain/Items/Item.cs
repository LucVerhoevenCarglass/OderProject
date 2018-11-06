using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Items
{
    public class Item
    {
        private const string ErrorMessage = "ItemDomain : ";
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal  Price { get; set; }
        public int StockAmount { get; set; }
        public int ToOrder { get; set; }
        public int OnOrder { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime LastDateOrder { get; set; }
        public DateTime LastDateSold { get; set; }


        public Item()
        {
            ItemId = Guid.NewGuid().ToString();
            Status = ItemStatus.Active;
        }

        public void CheckItemValues()
        {
            CheckFilledIn(Name, "Name");
            CheckFilledIn(Description, "Description");
            CheckNumFilledIn(Price, "Price");
            CheckNumFilledIn(StockAmount, "StockAmount");
        }


        private void CheckFilledIn(string stringValue, string errorMessageIfNotFilledIn)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                throw new OrderExeptions($"{ErrorMessage}{errorMessageIfNotFilledIn} is required");
        }

        private void CheckNumFilledIn(decimal NumValue, string errorMessageIfNotFilledIn)
        {
            if (NumValue < 0)
                throw new OrderExeptions($"{ErrorMessage}{errorMessageIfNotFilledIn} can not be negative");
        }
    }
}
