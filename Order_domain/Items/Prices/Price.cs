namespace Order_domain.Items.Prices
{
    public class Price
    {
        public decimal Amount { get; set; }

        private Price(decimal amount)
        {
            Amount = amount;
        }

        public static Price Create(decimal amount)
        {
            return new Price(amount);
        }

        public float GetAmountAsFloat()
        {
            return (float)Amount;
        }

        public static Price Add(Price price1, Price price2)
        {
            return Price.Create(decimal.Add(price1.Amount, price2.Amount));
        }

        public bool SameAs(Price otherPrice)
        {
            return Amount.Equals(otherPrice.Amount);
        }

        public override string ToString()
        {
            return "Price{" + "amount=" + Amount + '}';
        }
    }
}
