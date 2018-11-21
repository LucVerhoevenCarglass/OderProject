using Oder_infrastructure.builders;

namespace Order_domain.Customers.Addresses
{
    public class Address
    {
        public string StreetName { get; }
        public string HouseNumber { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(AddressBuilder addressBuilder)
        {
            StreetName = addressBuilder.StreetName;
            HouseNumber = addressBuilder.HouseNumber;
            PostalCode = addressBuilder.PostalCode;
            Country = addressBuilder.Country;
        }
        
        public override string ToString()
        {
            return "Address{" + "streetName='" + StreetName + '\'' +
                   ", houseNumber='" + HouseNumber + '\'' +
                   ", postalCode='" + PostalCode + '\'' +
                   ", country='" + Country + '\'' +
                   '}';
        }
    }

    public class AddressBuilder : Builder<Address> {

        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public static AddressBuilder Address()
        {
            return new AddressBuilder();
        }
        
        public override Address Build()
        {
            return new Address(this);
        }

        public AddressBuilder WithStreetName(string streetName)
        {
            StreetName = streetName;
            return this;
        }

        public AddressBuilder WithHouseNumber(string houseNumber)
        {
            HouseNumber = houseNumber;
            return this;
        }

        public AddressBuilder WithPostalCode(string postalCode)
        {
            PostalCode = postalCode;
            return this;
        }

        public AddressBuilder WithCountry(string country)
        {
            Country = country;
            return this;
        }
    }
}
