using Oder_infrastructure.builders;
using Order_domain.Customers.Addresses;

namespace Order_domain.tests.Customers.Addresses
{
    public class AddressTestBuilder : Builder<Address>
    {
        private readonly AddressBuilder _addressBuilder;

        private AddressTestBuilder(AddressBuilder addressBuilder)
        {
            _addressBuilder = addressBuilder;
        }

        public static AddressTestBuilder AnEmptyAddress()
        {
            return new AddressTestBuilder(AddressBuilder.Address());
        }

        public static AddressTestBuilder AnAddress()
        {
            return new AddressTestBuilder(AddressBuilder.Address()
                .WithCountry("Belgium")
                .WithHouseNumber("16A")
                .WithPostalCode("3000")
                .WithStreetName("Jantjesstraat"));
        }
        
        public override Address Build()
        {
            return _addressBuilder.Build();
        }

        public AddressTestBuilder WithStreetName(string streetName)
        {
            _addressBuilder.WithStreetName(streetName);
            return this;
        }

        public AddressTestBuilder WithHouseNumber(string houseNumber)
        {
            _addressBuilder.WithHouseNumber(houseNumber);
            return this;
        }

        public AddressTestBuilder WithPostalCode(string postalCode)
        {
            _addressBuilder.WithPostalCode(postalCode);
            return this;
        }

        public AddressTestBuilder WithCountry(string country)
        {
            _addressBuilder.WithCountry(country);
            return this;
        }
    }
}
