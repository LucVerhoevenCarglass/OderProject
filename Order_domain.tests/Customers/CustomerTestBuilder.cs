using System;
using Oder_infrastructure.builders;
using Order_domain.Customers;
using Order_domain.Customers.Addresses;
using Order_domain.Customers.Emails;
using Order_domain.Customers.PhoneNumbers;
using Order_domain.tests.Customers.Addresses;
using Order_domain.tests.Customers.Emails;
using Order_domain.tests.Customers.PhoneNumbers;

namespace Order_domain.tests.Customers
{
    public class CustomerTestBuilder : Builder<Customer>
    {
        private readonly Customer.CustomerBuilder _customerBuilder;

        private CustomerTestBuilder(Customer.CustomerBuilder customerBuilder)
        {
            _customerBuilder = customerBuilder;
        }

        public static CustomerTestBuilder AnEmptyCustomer()
        {
            return new CustomerTestBuilder(Customer.CustomerBuilder.Customer());
        }

        public static CustomerTestBuilder ACustomer()
        {
            return new CustomerTestBuilder(Customer.CustomerBuilder.Customer()
                .WithFirstname("Tom")
                .WithLastname("Thompson")
                .WithAddress(AddressTestBuilder.AnAddress().Build())
                .WithEmail(EmailTestBuilder.AnEmail().Build())
                .WithPhoneNumber(PhoneNumberTestBuilder.APhoneNumber().Build())
            );
        }

        public override Customer Build()
        {
            return _customerBuilder.Build();
        }

        public CustomerTestBuilder WithId(Guid id)
        {
            _customerBuilder.WithId(id);
            return this;
        }

        public CustomerTestBuilder WithFirstname(string firstname)
        {
            _customerBuilder.WithFirstname(firstname);
            return this;
        }

        public CustomerTestBuilder WithLastname(string lastname)
        {
            _customerBuilder.WithLastname(lastname);
            return this;
        }

        public CustomerTestBuilder WithEmail(Email email)
        {
            _customerBuilder.WithEmail(email);
            return this;
        }

        public CustomerTestBuilder WithAddress(Address address)
        {
            _customerBuilder.WithAddress(address);
            return this;
        }

        public CustomerTestBuilder WithPhoneNumber(PhoneNumber phoneNumber)
        {
            _customerBuilder.WithPhoneNumber(phoneNumber);
            return this;
        }
    }
}
