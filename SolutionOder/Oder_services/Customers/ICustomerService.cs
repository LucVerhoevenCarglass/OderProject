using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Customers;
using Order.Domain.Users;

namespace Order.Services.Customers
{
    public interface ICustomerService
    {
        void CreateNewCustomer(Customer CustomerToCreate);

        IEnumerable<Customer> GetAllCustomers();
        Customer GetDetailCustomer(string searchId);
    }
}
