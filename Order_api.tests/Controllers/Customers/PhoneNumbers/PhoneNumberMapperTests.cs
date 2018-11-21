using Order_api.Controllers.Customers.PhoneNumbers;
using Order_domain.Customers.PhoneNumbers;
using Xunit;

namespace Order_api.tests.Controllers.Customers.PhoneNumbers
{
    public class PhoneNumberMapperTests
    {
        [Fact]
        public void ToDto()
        {
            string number = "4848522541";
            string countryCallingCode = "+32";

            PhoneNumberDto phoneNumberDto = new PhoneNumberMapper().ToDto(PhoneNumber.PhoneNumberBuilder.PhoneNumber()
                .WithNumber(number)
                .WithCountryCallingCode(countryCallingCode)
                .Build());

            Assert.Equal(number, phoneNumberDto.Number);
            Assert.Equal(countryCallingCode, phoneNumberDto.CountryCallingCode);
        }

        [Fact]
        public void ToDomain()
        {
            string number = "4848522541";
            string countryCallingCode = "+32";

            PhoneNumber phoneNumber = new PhoneNumberMapper().ToDomain(new PhoneNumberDto()
                .WithNumber(number)
                .WithCountryCallingCode(countryCallingCode));

            Assert.Equal(number, phoneNumber.Number);
            Assert.Equal(countryCallingCode, phoneNumber.CountryCallingCode);
        }
    }
}
