namespace Order_api.Controllers.Orders.Dtos
{
    public class OrderAfterCreationDto
    {
        public string OrderId { get; set; }
        public float TotalPrice { get; set; }

        public OrderAfterCreationDto WithOrderId(string orderId)
        {
            OrderId = orderId;
            return this;
        }

        public OrderAfterCreationDto WithTotalPrice(float totalPrice)
        {
            TotalPrice = totalPrice;
            return this;
        }
    }
}
