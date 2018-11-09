using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Customers;

namespace Order.Databases
{
    public interface ICustomersDatabase
    {
        List<Customer> GetDatabase();
        void InitDatabase();
        void ClearDatabase();
        void AddCustomer(Customer newCustomer);
        void AddCustomerIfNotExist(Customer customerToCreate);
        Customer GetDetailCustomer(string searchId);
    }
}
