using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using Order.Api;
using Order.Api.Controllers.Users;

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
            var adminUsername = "admin@oder.com";
            var adminPassword = "admin";
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
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

            int countBefore = await AssertCountTable();

            var adminUsername = "";
            var adminPassword = "";
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
            var response = await _client.PostAsync("/api/user", stringContent);
            Assert.True(response.IsSuccessStatusCode);

            adminUsername = "admin@oder.com";
            adminPassword = "admin";
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
            int countAfter = await AssertCountTable();
            Assert.Equal(countBefore + 1, countAfter);
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

            int countBefore = await AssertCountTable();
            var response = await _client.PostAsync("/api/user", stringContent);

            Assert.False(response.IsSuccessStatusCode);
            int countAfter = await AssertCountTable();
            Assert.Equal(countBefore , countAfter);
        }

        private async Task<int> AssertCountTable()
        {
            var response = await _client.GetAsync("api/user");
            var responseString = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<UserDtoOverView>>(responseString);
            return list.Count;
        }

        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
