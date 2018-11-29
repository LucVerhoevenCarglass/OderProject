using Order_api.Controllers.Customers.Addresses;
using Order_domain.Customers.Addresses;
using Xunit;

namespace Order_api.tests.Controllers.Customers.Addresses
{
    public class AddressMapperTests
    {
        [Fact]
        public void ToDto()
        {
            string streetName = "Teststraat";
            string houseNumber = "88B";
            string postalCode = "3000";
            string country = "Belgium";

            AddressDto addressDto = new AddressMapper().ToDto(AddressBuilder.Address()
                .WithStreetName(streetName)
                .WithHouseNumber(houseNumber)
                .WithPostalCode(postalCode)
                .WithCountry(country)
                .Build());


            Assert.Equal(streetName, addressDto.StreetName);
            Assert.Equal(houseNumber, addressDto.HouseNumber);
            Assert.Equal(postalCode, addressDto.PostalCode);
            Assert.Equal(country, addressDto.Country);
        }

        [Fact]
        public void ToDomain()
        {
            string streetName = "Teststraat";
            string houseNumber = "88B";
            string postalCode = "3000";
            string country = "Belgium";

            Address address = new AddressMapper().ToDomain(new AddressDto()
                .WithStreetName(streetName)
                .WithHouseNumber(houseNumber)
                .WithPostalCode(postalCode)
                .WithCountry(country));

            Assert.Equal(streetName, address.StreetName);
            Assert.Equal(houseNumber, address.HouseNumber);
            Assert.Equal(postalCode, address.PostalCode);
            Assert.Equal(country, address.Country);
        }
    }
}
