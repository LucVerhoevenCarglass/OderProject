using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using Order.Api;
using Order.Api.Controllers.Customers;
using Order.Databases;
using Order.Domain.Customers;

namespace Order.IntigrationTests.Customers
{
    public class CustomerControllerIntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CustomerControllerIntegrationTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            UsersDatabase.InitDatabase();
            CustomersDatabase.Customers.Clear();
            CustomersDatabase.Customers.Add(new Customer("00001"));
            CustomersDatabase.Customers.Add(new Customer("00002"));
            CustomersDatabase.Customers.Add(new Customer("00003"));
            CustomersDatabase.Customers.Add(new Customer("00004"));
        }


        [Fact]
        public async Task GetAllCustomers_WhenAdminUser_ThenReturnListOfAllCustomers()
        {
            var adminUsername = UsersDatabase.Users[0].Email;
            var adminPassword = UsersDatabase.Users[0].Password;
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
       
            var response = await _client.GetAsync("api/Customer");
            var responseString = await response.Content.ReadAsStringAsync();
            var customerList = JsonConvert.DeserializeObject<List<CustomerDtoOverView>>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(4, customerList.Count);
        }

        [Fact]
        public async Task GetCustomerDetail_WhenSearchCustomerIdAsAdminUser_ThenReturnCustomerInfo()
        {
            var adminUsername = UsersDatabase.Users[0].Email;
            var adminPassword = UsersDatabase.Users[0].Password;
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
            var response = await _client.GetAsync("api/Customer/00002");
            var responseString = await response.Content.ReadAsStringAsync();
            var customerDto = JsonConvert.DeserializeObject<CustomerDtoOverView>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("00002", customerDto.CustomerId);
        }

        [Fact]
        public async Task GetCustomerDetail__WhenSearchNonExistingCustomerIdAsAdminUser_ThenReturnBadRequest()
        {
            var adminUsername = UsersDatabase.Users[0].Email;
            var adminPassword = UsersDatabase.Users[0].Password;
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
            var response = await _client.GetAsync("api/Customer/00100");
            Assert.False(response.IsSuccessStatusCode);
        }

        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
