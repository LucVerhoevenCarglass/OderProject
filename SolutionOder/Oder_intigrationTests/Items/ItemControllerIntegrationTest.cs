using System;
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

            UsersDatabase.InitDatabase();
            ItemsDatabase.Items.Clear();
            ItemsDatabase.Items.Add(new Item() {Name = "ProdExisting"});
        }


        [Fact]
        public void CreateNewItem_WhenAdminUserAddNonExistingItem_ThenReturnSuccess()
        {
            var adminUsername = UsersDatabase.Users[0].Email;
            var adminPassword = UsersDatabase.Users[0].Password;
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
            
            ItemDtoToCreate itemToCreate = new ItemDtoToCreate()
            {
                Name="NewProduce",
                Description="NewDescription"
            };
            var content = JsonConvert.SerializeObject(itemToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = _client.PostAsync("api/item", stringContent);
            var result = response.Result;

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(2, ItemsDatabase.Items.Count);
        }

        [Fact]
        public async Task CreateNewItem_WhenAdminUserAddExistingItem_ThenReturnFalse()
        {
            var adminUsername = UsersDatabase.Users[0].Email;
            var adminPassword = UsersDatabase.Users[0].Password;
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(adminUsername, adminPassword);
            
            ItemDtoToCreate itemToCreate = new ItemDtoToCreate()
            {
                Name = "ProdExisting",
                Description = "NewDescription"
            };

            var content = JsonConvert.SerializeObject(itemToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/item", stringContent);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Single(ItemsDatabase.Items);
        }



        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
