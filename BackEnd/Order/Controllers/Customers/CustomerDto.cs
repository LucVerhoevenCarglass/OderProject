using System;
using Oder_infrastructure.builders;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;

namespace Order_api.Controllers.Customers
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmailDto Email { get; set; }
        public AddressDto Address { get; set; }
        public PhoneNumberDto PhoneNumber { get; set; }
    }

    public class CustomerDtoBuilder : Builder<CustomerDto>
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private EmailDto _email;
        private AddressDto _address;
        private PhoneNumberDto _phoneNumber;

        public static CustomerDtoBuilder CustomerDto()
        {
            return new CustomerDtoBuilder();
        }

        public override CustomerDto Build()
        {
            return new CustomerDto
            {
                Address = _address,
                Email = _email,
                FirstName = _firstName,
                LastName = _lastName,
                Id = _id.ToString("N"),
                PhoneNumber = _phoneNumber
            };
        }

        public CustomerDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CustomerDtoBuilder WithFirstname(string firstname)
        {
            _firstName = firstname;
            return this;
        }

        public CustomerDtoBuilder WithLastname(string lastname)
        {
            _lastName = lastname;
            return this;
        }

        public CustomerDtoBuilder WithEmail(EmailDto email)
        {
            _email = email;
            return this;
        }

        public CustomerDtoBuilder WithAddress(AddressDto address)
        {
            _address = address;
            return this;
        }

        public CustomerDtoBuilder WithPhoneNumber(PhoneNumberDto phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }
    }
}
