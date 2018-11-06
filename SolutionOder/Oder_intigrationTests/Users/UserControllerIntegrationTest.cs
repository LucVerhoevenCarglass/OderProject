using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using Order.Api;
using Order.Api.Controllers.Users;
using Order.Databases;

namespace Order.IntigrationTests.Users
{
    public class UserControllerIntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UserControllerIntegrationTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            UsersDatabase.InitDatabase();
            CustomersDatabase.Customers.Clear();
        }

        [Fact]
        public async Task RegisterNewUser_WithValidInformation_shouldCreate()
        {
            UserDtoToCreate newUser = new UserDtoToCreate
            {
                City = "City",
                Email = "Email@test.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "1Password123",
                StreetAndNumber = "StreetNumber",
                Telephone = "Telephone",
                Zip = "Zip"
            };
            var content = JsonConvert.SerializeObject(newUser);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            Assert.Equal(0, UsersDatabase.Users.Count(user => user.Email == "Email@test.com"));
            Assert.Equal(0, CustomersDatabase.Customers.Count(cust => cust.Email == "Email@test.com"));


            var response = await _client.PostAsync("/api/user", stringContent);

            Assert.True(response.IsSuccessStatusCode);

            Assert.Equal(1, UsersDatabase.Users.Count(user=> user.Email== "Email@test.com"));
            Assert.Equal(1, CustomersDatabase.Customers.Count(cust => cust.Email == "Email@test.com"));
        }

        [Fact]
        public async Task RegisterNewUser_WithInValidInformation_shouldNotBeAdded()
        {
            UserDtoToCreate newUser = new UserDtoToCreate
            {
                City = "City",
                Email = "NO EMAIL",
                Password = "1Password123",
            };
            var content = JsonConvert.SerializeObject(newUser);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/user", stringContent);

            Assert.False(response.IsSuccessStatusCode);

            Assert.Equal(0, UsersDatabase.Users.Count(user => user.Email == "NO EMAIL"));
        }
    }
}
