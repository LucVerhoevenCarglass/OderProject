using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Customers
{
    public class Customer
    {
        public string CustomerId { get; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAndNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }

        public Customer( string customerId)
        {
            CustomerId = customerId;
        }

        public void CheckCustomerValues()
        {
            CheckFilledIn(FirstName, "First name");
            CheckFilledIn(LastName, "Last name");
            CheckFilledIn(StreetAndNumber, "Street and number");
            CheckFilledIn(Zip, "ZipCode");
            CheckFilledIn(City, "City");
            CheckFilledIn(Telephone, "Telephone");
        }

        private void CheckFilledIn(string stringValue, string errorMessageIfNotFilledIn)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                throw new OrderExeptions($"CustomerDomain: {errorMessageIfNotFilledIn} is required");
        }

    }
}
