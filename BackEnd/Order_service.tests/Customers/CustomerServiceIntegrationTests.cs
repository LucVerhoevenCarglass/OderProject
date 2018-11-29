using System;
using System.Linq;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Order_service.Customers;
using Order_service.Data;
using Xunit;

namespace Order_service.tests.Customers
{
    public class CustomerServiceIntegrationTests : IDisposable
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        public void Dispose()
        {
            _customerRepository.Reset();
        }

        //public CustomerServiceIntegrationTests()
        //{
        //    _customerRepository = new CustomerRepository(new CustomerDatabase());
        //    _customerService = new CustomerService(_customerRepository, new CustomerValidator());
        //}



        //[Fact]
        //public void CreateCustomer()
        //{
        //    Customer customerToCreate = CustomerTestBuilder.ACustomer().Build();

        //    Customer createdCustomer = _customerService.CreateCustomer(customerToCreate);

        //    var customer = _customerRepository.Get(customerToCreate.Id);

        //    Assert.Equal(customerToCreate, customer);
        //    Assert.Equal(createdCustomer, customer);
        //}

        //[Fact]
        //public void GetAllCustomers()
        //{
        //    Customer customer1 = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
        //    Customer customer2 = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
        //    Customer customer3 = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());

        //    var allCustomers = _customerService.GetAllCustomers().ToList();

        //    Assert.Contains(customer1, allCustomers);
        //    Assert.Contains(customer2, allCustomers);
        //    Assert.Contains(customer3, allCustomers);
        //}

        //[Fact]
        //public void GetCustomer()
        //{
        //    _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
        //    Customer customerToFind = _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
        //    _customerService.CreateCustomer(CustomerTestBuilder.ACustomer().Build());
        //    Customer foundCustomer = _customerService.GetCustomer(customerToFind.Id);

        //    Assert.Equal(customerToFind, foundCustomer);
        //}
    }
}
