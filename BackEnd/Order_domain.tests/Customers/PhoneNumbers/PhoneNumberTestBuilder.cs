using Oder_infrastructure.builders;
using Order_domain.Customers.PhoneNumbers;

namespace Order_domain.tests.Customers.PhoneNumbers
{
    public class PhoneNumberTestBuilder : Builder<PhoneNumber>
    {
        private readonly PhoneNumber.PhoneNumberBuilder _phoneNumberBuilder;

        private PhoneNumberTestBuilder(PhoneNumber.PhoneNumberBuilder phoneNumberBuilder)
        {
            _phoneNumberBuilder = phoneNumberBuilder;
        }

        public static PhoneNumberTestBuilder AnEmptyPhoneNumber()
        {
            return new PhoneNumberTestBuilder(PhoneNumber.PhoneNumberBuilder.PhoneNumber());
        }

        public static PhoneNumberTestBuilder APhoneNumber()
        {
            return new PhoneNumberTestBuilder(PhoneNumber.PhoneNumberBuilder.PhoneNumber()
                .WithNumber("484554433")
                .WithCountryCallingCode("+32")
            );
        }

        public override PhoneNumber Build()
        {
            return _phoneNumberBuilder.Build();
        }

        public PhoneNumberTestBuilder WithNumber(string number)
        {
            _phoneNumberBuilder.WithNumber(number);
            return this;
        }

        public PhoneNumberTestBuilder WithCountryCallingCode(string countryCallingCode)
        {
            _phoneNumberBuilder.WithCountryCallingCode(countryCallingCode);
            return this;
        }
    }
}
