namespace Order_api.Controllers.Customers.PhoneNumbers
{
    public class PhoneNumberDto
    {
        public string Number { get; set; }

        public string CountryCallingCode { get; set; }
        
        public PhoneNumberDto WithNumber(string number)
        {
            Number = number;
            return this;
        }

        public PhoneNumberDto WithCountryCallingCode(string countryCallingCode)
        {
            CountryCallingCode = countryCallingCode;
            return this;
        }
    }
}
