using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;
using Order.Api;
using Order.Api.Controllers.Customers;
using Order.Databases;
using Order.Domain.Customers;
using Order.Domain.Users;
using NLog;
using NSubstitute;

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

            var adminUsername = "admin@oder.com";
            var adminPassword = "admin";
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
        }

        [Fact]
        public async Task GetAllCustomers_WhenAdminUser_ThenReturnListOfAllCustomers()
        {
            var response = await _client.GetAsync("api/Customer");
            var responseString = await response.Content.ReadAsStringAsync();
            var customerList = JsonConvert.DeserializeObject<List<CustomerDtoOverView>>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Single(customerList);
        }

        [Fact]
        public async Task GetCustomerDetail_WhenSearchCustomerIdAsAdminUser_ThenReturnCustomerInfo()
        {
            var response = await _client.GetAsync("api/Customer/IdCustomer");
            var responseString = await response.Content.ReadAsStringAsync();
            var customerDto = JsonConvert.DeserializeObject<CustomerDtoOverView>(responseString);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("IdCustomer", customerDto.CustomerId);
        }

        [Fact]
        public async Task GetCustomerDetail__WhenSearchNonExistingCustomerIdAsAdminUser_ThenReturnBadRequest()
        {
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
