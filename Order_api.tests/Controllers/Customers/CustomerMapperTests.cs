using System;
using Order_api.Controllers.Customers;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;
using Order_domain.Customers;
using Order_domain.Customers.Addresses;
using Order_domain.Customers.Emails;
using Order_domain.Customers.PhoneNumbers;
using Order_domain.tests.Customers.Addresses;
using Order_domain.tests.Customers.Emails;
using Order_domain.tests.Customers.PhoneNumbers;
using Xunit;

namespace Order_api.tests.Controllers.Customers
{
    public class CustomerMapperTests
    {
        private readonly CustomerMapper _customerMapper;

        public CustomerMapperTests()
        {
            _customerMapper = new CustomerMapper(new AddressMapper(), new EmailMapper(), new PhoneNumberMapper());
        }

        [Fact]
        public void ToDto()
        {
            Guid customerId = Guid.NewGuid();
            string firstname = "Koen";
            string lastname = "Kasteels";
            
            Address address = AddressTestBuilder.AnAddress().Build();
            Email email = EmailTestBuilder.AnEmail().Build();
            PhoneNumber phoneNumber = PhoneNumberTestBuilder.APhoneNumber().Build();
            
            Customer customer = Customer.CustomerBuilder.Customer()
                    .WithId(customerId)
                    .WithFirstname(firstname)
                    .WithLastname(lastname)
                    .WithAddress(address)
                    .WithEmail(email)
                    .WithPhoneNumber(phoneNumber)
                    .Build();

            // when
            CustomerDto customerDto = _customerMapper.ToDto(customer);

            // then
            Assert.Equal(customerId.ToString("N"), customerDto.Id);
            Assert.Equal(firstname, customerDto.FirstName);
            Assert.Equal(lastname, customerDto.LastName);

            Assert.Equal("Jantjesstraat", customerDto.Address.StreetName);
            Assert.Equal("16A", customerDto.Address.HouseNumber);
            Assert.Equal("3000", customerDto.Address.PostalCode);
            Assert.Equal("Belgium", customerDto.Address.Country);

            Assert.Equal("niels", customerDto.Email.LocalPart);
            Assert.Equal("mymail.be", customerDto.Email.Domain);
            Assert.Equal("niels@mymail.be", customerDto.Email.Complete);

            Assert.Equal("484554433", customerDto.PhoneNumber.Number);
            Assert.Equal("+32", customerDto.PhoneNumber.CountryCallingCode);
        }

        [Fact]
        public void ToDomain()
        {
            Guid customerId = Guid.NewGuid();
            string firstname = "Tim";
            string lastname = "Timmelston";

            string streetName = "streetName";
            string houseNumber = "HouseNumber";
            string postalCode = "postalCode";
            string country = "country";

            AddressDto addressDto = new AddressDto()
                .WithStreetName(streetName)
                .WithHouseNumber(houseNumber)
                .WithPostalCode(postalCode)
                .WithCountry(country);

            string localpart = "localPart";
            string domain = "domain";
            string complete = "complete";

            EmailDto emailDto = new EmailDto()
                .WithLocalPart(localpart)
                .WithDomain(domain)
                .WithComplete(complete);

            string countryCallingCode = "32";
            string number = "number";

            PhoneNumberDto phoneNumberDto = new PhoneNumberDto()
                .WithCountryCallingCode(countryCallingCode)
                .WithNumber(number);

            // when
            Customer customer = _customerMapper.ToDomain(CustomerDtoBuilder.CustomerDto()
                .WithId(customerId)
                .WithFirstname(firstname)
                .WithLastname(lastname)
                .WithAddress(addressDto)
                .WithEmail(emailDto)
                .WithPhoneNumber(phoneNumberDto)
                .Build());

            // then
            Assert.Equal(customerId, customer.Id);
            Assert.Equal(firstname, customer.FirstName);
            Assert.Equal(lastname, customer.LastName);

            Assert.Equal(streetName, customer.Address.StreetName);
            Assert.Equal(houseNumber, customer.Address.HouseNumber);
            Assert.Equal(postalCode, customer.Address.PostalCode);
            Assert.Equal(country, customer.Address.Country);

            Assert.Equal(localpart, customer.Email.LocalPart);
            Assert.Equal(domain, customer.Email.Domain);
            Assert.Equal(complete, customer.Email.Complete);

            Assert.Equal(number, customer.PhoneNumber.Number);
            Assert.Equal(countryCallingCode, customer.PhoneNumber.CountryCallingCode);
        }
    }
}
