using System;
using System.Collections.Generic;
using Order_domain.Customers;

namespace Order_service.Customers
{
    public interface ICustomerService
    {
        Customer CreateCustomer(Customer customer);

        IEnumerable<Customer> GetAllCustomers();

        Customer GetCustomer(Guid id);
    }
}