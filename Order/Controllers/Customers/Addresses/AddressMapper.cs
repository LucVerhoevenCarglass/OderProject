using Oder_infrastructure.dto;
using Order_domain.Customers.Addresses;

namespace Order_api.Controllers.Customers.Addresses
{
    public class AddressMapper : Mapper<AddressDto, Address>
    {
        public override AddressDto ToDto(Address address)
        {
            return new AddressDto()
                .WithStreetName(address.StreetName)
                .WithHouseNumber(address.HouseNumber)
                .WithPostalCode(address.PostalCode)
                .WithCountry(address.Country);
        }
        public override Address ToDomain(AddressDto addressDto)
        {
            return AddressBuilder.Address()
                .WithStreetName(addressDto.StreetName)
                .WithHouseNumber(addressDto.HouseNumber)
                .WithPostalCode(addressDto.PostalCode)
                .WithCountry(addressDto.Country)
                .Build();
        }
    }
}