using Oder_infrastructure.dto;
using Order_domain.Customers.PhoneNumbers;

namespace Order_api.Controllers.Customers.PhoneNumbers
{
    public class PhoneNumberMapper : Mapper<PhoneNumberDto, PhoneNumber>
    {
        public override PhoneNumberDto ToDto(PhoneNumber phoneNumber)
        {
            return new PhoneNumberDto()
                .WithNumber(phoneNumber.Number)
                .WithCountryCallingCode(phoneNumber.CountryCallingCode);
        }

        public override PhoneNumber ToDomain(PhoneNumberDto phoneNumberDto)
        {
            return PhoneNumber.PhoneNumberBuilder.PhoneNumber()
                .WithNumber(phoneNumberDto.Number)
                .WithCountryCallingCode(phoneNumberDto.CountryCallingCode)
                .Build();
        }
    }
}
