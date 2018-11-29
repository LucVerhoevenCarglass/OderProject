namespace Order_api.Controllers.Orders.Dtos.Reports
{
    public class ItemGroupReportDto
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public int OrderedAmount { get; set; }
        public float TotalPrice { get; set; }

        public ItemGroupReportDto WithItemId(string itemId)
        {
            ItemId = itemId;
            return this;
        }

        public ItemGroupReportDto WithOrderedAmount(int orderedAmount)
        {
            OrderedAmount = orderedAmount;
            return this;
        }

        public ItemGroupReportDto WithName(string name)
        {
            Name = name;
            return this;
        }

        public ItemGroupReportDto WithTotalPrice(float totalPrice)
        {
            TotalPrice = totalPrice;
            return this;
        }
    }
}
