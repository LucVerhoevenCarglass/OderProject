namespace Order_api.Controllers.Customers.Addresses
{
    public class AddressDto
    {
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public AddressDto WithStreetName(string streetName)
        {
            StreetName = streetName;
            return this;
        }

        public AddressDto WithHouseNumber(string houseNumber)
        {
            HouseNumber = houseNumber;
            return this;
        }

        public AddressDto WithPostalCode(string postalCode)
        {
            PostalCode = postalCode;
            return this;
        }

        public AddressDto WithCountry(string country)
        {
            Country = country;
            return this;
        }
    }
}
