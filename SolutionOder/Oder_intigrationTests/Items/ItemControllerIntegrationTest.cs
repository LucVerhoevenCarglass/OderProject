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
using Order.Api.Controllers.Items;
using Order.Databases;
using Order.Domain.Items;
using Order.Domain.Users;

namespace Order.IntigrationTests.Items
{
    public class ItemControllerIntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ItemControllerIntegrationTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var adminUsername = "admin@oder.com";
            var adminPassword = "admin";
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);

 //           ItemsDatabase.Items.Clear();
 //           ItemsDatabase.Items.Add(new Item() {Name = "ProdExisting"});
        }


        [Fact]
        public async Task CreateNewItem_WhenAdminUserAddNonExistingItem_ThenReturnSuccess()
        {
            int countBefore = await AssertCountTable();
            ItemDtoToCreate itemToCreate = new ItemDtoToCreate()
            {
                Name="NewProduce",
                Description="NewDescription"
            };
            var content = JsonConvert.SerializeObject(itemToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync("api/item", stringContent);
            Assert.True(response.IsSuccessStatusCode);
            int countAfter = await AssertCountTable();
            Assert.Equal(countBefore+1, countAfter);
        }

        [Fact]
        public async Task CreateNewItem_WhenAdminUserAddExistingItem_ThenReturnFalse()
        {
            int countBefore = await AssertCountTable();
            ItemDtoToCreate itemToCreate = new ItemDtoToCreate()
            {
                Name = "Product1",
                Description = "NewDescription"
            };

            var content = JsonConvert.SerializeObject(itemToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/item", stringContent);

            Assert.False(response.IsSuccessStatusCode);
            int countAfter = await AssertCountTable();
            Assert.Equal(countBefore, countAfter);
        }

        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        private async Task<int> AssertCountTable()
        {
            var response = await _client.GetAsync("api/item");
            var responseString = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ItemDtoOverView>>(responseString);
            return list.Count;
        }
    }
}
