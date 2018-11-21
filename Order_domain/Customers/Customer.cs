using System;
using Oder_infrastructure.builders;
using Order_domain.Customers.Addresses;
using Order_domain.Customers.Emails;
using Order_domain.Customers.PhoneNumbers;

namespace Order_domain.Customers
{
    public class Customer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }

        public Address Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public Customer(CustomerBuilder customerBuilder)
            :base(customerBuilder.Id)
        {
            FirstName = customerBuilder.FirstName;
            LastName = customerBuilder.LastName;
            Email = customerBuilder.Email;
            Address = customerBuilder.Address;
            PhoneNumber = customerBuilder.PhoneNumber;
        }

        public override string ToString()
        {
            return "Customer{" +
                    "id='" + Id.ToString("N") + '\'' +
                    ", firstname='" + FirstName + '\'' +
                    ", lastname='" + LastName + '\'' +
                    ", email=" + Email +
                    ", address=" + Address +
                    ", phoneNumber=" + PhoneNumber +
                    '}';
        }

        public class CustomerBuilder : Builder<Customer>
        {

            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Email Email { get; set; }
            public Address Address { get; set; }
            public PhoneNumber PhoneNumber { get; set; }
            
        public static CustomerBuilder Customer()
        {
            return new CustomerBuilder();
        }
            
        public override Customer Build()
        {
            return new Customer(this);
        }

        public CustomerBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public CustomerBuilder WithFirstname(string firstname)
        {
            FirstName = firstname;
            return this;
        }

        public CustomerBuilder WithLastname(string lastname)
        {
            LastName = lastname;
            return this;
        }

        public CustomerBuilder WithEmail(Email email)
        {
            Email = email;
            return this;
        }

        public CustomerBuilder WithAddress(Address address)
        {
            Address = address;
            return this;
        }

        public CustomerBuilder WithPhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

    }
}
}
