using System;
using System.Collections.Generic;
using System.Linq;
using Order_domain.Customers;

namespace Order_service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerValidator _customerValidator;

        
        public CustomerService(ICustomerRepository customerRepository, CustomerValidator customerValidator)
        {
            _customerRepository = customerRepository;
            _customerValidator = customerValidator;
        }

        public Customer CreateCustomer(Customer customer)
        {
            if (!_customerValidator.IsValidForCreation(customer))
            {
                _customerValidator.ThrowInvalidOperationException(customer, "creation");
            }
            return _customerRepository.Save(customer);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomer(Guid id)
        {
            return _customerRepository.Get(id);
        }
    }
}
