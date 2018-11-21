using System;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Order_service.Customers;
using Xunit;

namespace Order_service.tests.Customers
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly CustomerRepository _customerRepository;

        public CustomerServiceTests()
        {
            _customerRepository = new CustomerRepository(new CustomerDatabase());
            _customerService = new CustomerService(_customerRepository, new CustomerValidator());
        }

        [Fact]
        public void CreateCustomer_HappyPath()
        {
            Customer customer = CustomerTestBuilder.ACustomer().Build();

            Customer createdCustomer = _customerService.CreateCustomer(customer);

            Assert.NotNull(createdCustomer);
            Assert.Equal(customer, _customerRepository.Get(createdCustomer.Id));
        }

        [Fact]
        public void CreateCustomer_GivenCustomerThatIsNotValidForCreation_ThenThrowException()
        {
            Customer customer = CustomerTestBuilder.AnEmptyCustomer().Build();

            Exception ex = Assert.Throws<InvalidOperationException>(() => _customerService.CreateCustomer(customer));
            Assert.Equal("Invalid Customer provided for creation. Provided object: Customer{id='" + customer.Id.ToString("N")
                                                                                                  + "', firstname='" + customer.FirstName
                                                                                                  + "', lastname='" + customer.LastName
                                                                                                  + "', email=" + customer.Email + ", address="
                                                                                                  + customer.Address + ", phoneNumber="
                                                                                                  + customer.PhoneNumber + "}", ex.Message);
        }
    }
}
