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

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }
        public void CreateNewCustomer(Customer customerToCreate)
        {
            if (CustomersDatabase.Customers.Any(cust => cust.CustomerId == customerToCreate.CustomerId))
            {
                _logger.LogError($"{ErrorMessage} Id already exists: { customerToCreate.CustomerId}");
                throw new OrderExeptions($"{ErrorMessage} Id already exists");
            }
            CustomersDatabase.Customers.Add(customerToCreate);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return CustomersDatabase.Customers;
        }

        public Customer GetDetailCustomer(string searchId)
        {
            var findCustomer= CustomersDatabase.Customers.Find(cust => cust.CustomerId == searchId);
            if (findCustomer==null)
            {
                throw new OrderExeptions($"{ErrorMessage} CustomerId {searchId} does not exists");
            }
            return findCustomer;
        }
    }
}
