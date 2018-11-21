using Order_domain.Items.Prices;
using Xunit;

namespace Order_domain.tests.Items.Prices
{
    public class PriceTests
    {
        [Fact]
        public void Add()
        {
            Price price1 = Price.Create(new decimal(10));
            Price price2 = Price.Create(new decimal(10.50));

            Price priceCombined = Price.Add(price1, price2);

            Assert.Equal(new decimal(20.50), priceCombined.Amount);
        }

        [Fact]
        public void SameAs()
        {
            Price price1 = Price.Create(new decimal(10));
            Price price2 = Price.Create(new decimal(10));

            Assert.True(price1.SameAs(price2));
        }
    }
}
