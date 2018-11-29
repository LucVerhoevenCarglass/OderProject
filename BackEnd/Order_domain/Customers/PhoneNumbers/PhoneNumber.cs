
using Oder_infrastructure.builders;

namespace Order_domain.Customers.PhoneNumbers
{
    public class PhoneNumber
    {
        public string Number { get; set; }
        public string CountryCallingCode { get; set; }

        private PhoneNumber()
        {
            
        }
        private PhoneNumber(PhoneNumberBuilder phoneNumberBuilder)
        {
            Number = phoneNumberBuilder.Number;
            CountryCallingCode = phoneNumberBuilder.CountryCallingCode;
        }

        public override string ToString()
        {
            return "PhoneNumber{" + "number='" + Number + '\'' +
                   ", countryCallingCode='" + CountryCallingCode + '\'' +
                   '}';
        }

        public class PhoneNumberBuilder : Builder<PhoneNumber>
        {
            public string Number { get; set; }
            public string CountryCallingCode { get; set; }

            public static PhoneNumberBuilder PhoneNumber()
            {
                return new PhoneNumberBuilder();
            }

            public override PhoneNumber Build()
            {
                return new PhoneNumber(this);
            }

            public PhoneNumberBuilder WithNumber(string number)
            {
                Number = number;
                return this;
            }

            public PhoneNumberBuilder WithCountryCallingCode(string countryCallingCode)
            {
                CountryCallingCode = countryCallingCode;
                return this;
            }
        }
    }
}
