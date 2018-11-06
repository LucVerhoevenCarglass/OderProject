using System.Collections.Generic;
using Order.Domain.Customers;

namespace Order.Databases
{
    public static class CustomersDatabase
    {
        public static List<Customer> Customers = new List<Customer>();

        public static void InitDatabase()
        {
            Customers.Add(new Customer(UsersDatabase.Users[1].UserId)
            {
                Email=UsersDatabase.Users[1].Email,
                City="City",
                FirstName="FirstName",
                LastName="LastName",
                StreetAndNumber="Street 5",
                Telephone="03",
                Zip="2580"               
            });
        }
    }
}
