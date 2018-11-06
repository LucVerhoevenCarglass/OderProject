using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain;
using Order.Domain.Customers;
using Xunit;

namespace Order.Domain.UnitTests.Customers
{
    public class CustomerTest
    {
        [Fact]
        public void GivenCustomerDatabase_WhenCreateNewCustomer_ThenInputFirstNameHasToBeFilledIn()
        {
            Customer newCustomer = new Customer("ID")
            {
                FirstName = "",
                LastName = "LastName",
                City = "City",
                Email = "Email",
                StreetAndNumber = "StreetAndNumber",
                Telephone = "03",
                Zip = "Zip"
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newCustomer.CheckCustomerValues());
            Assert.Contains("is required", errorExeption.Message);
        }

        [Fact]
        public void GivenCustomerDatabase_WhenCreateNewCustomer_ThenInputLastNameHasToBeFilledIn()
        {
            Customer newCustomer = new Customer("ID")
            {
                FirstName = "FirstName",
                LastName = "",
                City = "City",
                Email = "Email",
                StreetAndNumber = "StreetAndNumber",
                Telephone = "03",
                Zip = "Zip"
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newCustomer.CheckCustomerValues());
            Assert.Contains("is required", errorExeption.Message);
        }

        [Fact]
        public void GivenCustomerDatabase_WhenCreateNewCustomer_ThenInputCityHasToBeFilledIn()
        {
            Customer newCustomer = new Customer("ID")
            {
                FirstName = "FirstName",
                LastName = "LastName",
                City = "",
                Email = "Email",
                StreetAndNumber = "StreetAndNumber",
                Telephone = "03",
                Zip = "Zip"
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newCustomer.CheckCustomerValues());
            Assert.Contains("is required", errorExeption.Message);
        }
        
        [Fact]
        public void GivenCustomerDatabase_WhenCreateNewCustomer_ThenInputStreetHasToBeFilledIn()
        {
            Customer newCustomer = new Customer("ID")
            {
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Email = "Email",
                StreetAndNumber = "",
                Telephone = "03",
                Zip = "Zip"
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newCustomer.CheckCustomerValues());
            Assert.Contains("is required", errorExeption.Message);
        }

        [Fact]
        public void GivenCustomerDatabase_WhenCreateNewCustomer_ThenInputTelephoneHasToBeFilledIn()
        {

            Customer newCustomer = new Customer("ID")
            {
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Email = "Email",
                StreetAndNumber = "StreetAndNumber",
                Telephone = "",
                Zip = "Zip"
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newCustomer.CheckCustomerValues());
            Assert.Contains("is required", errorExeption.Message);
        }

        [Fact]
        public void GivenCustomerDatabase_WhenCreateNewCustomer_ThenInputZipHasToBeFilledIn()
        {
            Customer newCustomer = new Customer("ID")
            {
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Email = "Email",
                StreetAndNumber = "StreetAndNumber",
                Telephone = "03",
                Zip = ""
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newCustomer.CheckCustomerValues());
            Assert.Contains("is required", errorExeption.Message);
        }
    }
}
