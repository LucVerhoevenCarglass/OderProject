using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Order.Databases;
using Order.Domain;
using Order.Domain.Customers;

namespace Order.Services.Customers
{
    public class CustomerService: ICustomerService
    {
        private const string ErrorMessage = "CustomerModule : ";
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomersDatabase _customersDatabase;

        public CustomerService(ILogger<CustomerService> logger, ICustomersDatabase customersDatabase)
        {
            _logger = logger;
            _customersDatabase = customersDatabase;
        }
        public void CreateNewCustomer(Customer customerToCreate)
        {
            _customersDatabase.AddCustomerIfNotExist(customerToCreate);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customersDatabase.GetDatabase();
        }

        public Customer GetDetailCustomer(string searchId)
        {
            return _customersDatabase.GetDetailCustomer(searchId);
        }
    }
}
