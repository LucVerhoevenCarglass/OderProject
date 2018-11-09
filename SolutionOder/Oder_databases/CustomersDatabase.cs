using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Order.Domain.Customers;
using Order.Domain.Users;

namespace Order.Databases
{
    public class CustomersDatabase: ICustomersDatabase
    {
        
        private readonly IUsersDatabase _usersDatabase;
        private readonly User _customerUser;
        private const string ErrorMessage = "CustomerDatabase : ";
        private readonly List<Customer> Customers = new List<Customer>();
        private readonly ILogger<CustomersDatabase> _logger;

        public CustomersDatabase(ILogger<CustomersDatabase> logger, IUsersDatabase usersDatabase)
        {
            _logger = logger;
            _usersDatabase = usersDatabase;
            _customerUser = usersDatabase.GetDatabase()[1];
            InitDatabase();
        }

        public List<Customer> GetDatabase()
        {
            return Customers;
        }

        public void ClearDatabase()
        {
            Customers.Clear();
        }

        public void AddCustomer(Customer newCustomer)
        {
            Customers.Add(newCustomer);
        }

        public void AddCustomerIfNotExist(Customer customerToCreate)
        {
            if (Customers.Any(cust => cust.CustomerId == customerToCreate.CustomerId))
            {
                _logger.LogError($"{ErrorMessage} Id already exists: { customerToCreate.CustomerId}");
                throw new OrderExeptions($"{ErrorMessage} Id already exists");
            }
            AddCustomer(customerToCreate);
        }

        public Customer GetDetailCustomer(string searchId)
        {
            var findCustomer = Customers.Find(cust => cust.CustomerId == searchId);
            if (findCustomer == null)
            {
                throw new OrderExeptions($"{ErrorMessage} CustomerId {searchId} does not exists");
            }
            return findCustomer;
        }

        public void InitDatabase()
        {
            Customers.Add(new Customer(_customerUser.UserId)
            {
                Email = _customerUser.Email,
                City = "City",
                FirstName = "FirstName",
                LastName = "LastName",
                StreetAndNumber = "Street 5",
                Telephone = "03",
                Zip = "2580"
            });
        }
    }
}
