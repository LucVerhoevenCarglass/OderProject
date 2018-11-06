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
using Order.Api.Controllers.Orders;
using Order.Databases;
using Order.Domain.Items;

namespace Order.IntigrationTests.Orders
{
    public class OrderControllerIntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly string testCustomerId;

        public OrderControllerIntegrationTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            UsersDatabase.InitDatabase();
            ItemsDatabase.InitDatabase();
            CustomersDatabase.InitDatabase();
            ////OrdersDatabase.Orders.Clear();


            testCustomerId = UsersDatabase.Users[1].UserId;
            var customerUsername = UsersDatabase.Users[1].Email;
            var customerPassword = UsersDatabase.Users[1].Password;
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader(customerUsername, customerPassword);
        }


        [Fact]
        public async Task CreateNewOrder_WhenExistingCustomerCreateNewOrder_OrderAddedToDatabase()
        {
            List<ItemDtoToOrder> itemsToOrder = new List<ItemDtoToOrder>()
            {
                new ItemDtoToOrder()
                {
                    Name = "Product1",
                    OrderAmount = 10
                },
                new ItemDtoToOrder()
                {
                    Name = "Product2",
                    OrderAmount = 5
                }
            };
            var content = JsonConvert.SerializeObject(itemsToOrder);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"api/order/{testCustomerId}", stringContent);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Single(OrdersDatabase.Orders);

             response = await _client.GetAsync($"api/order/{testCustomerId}");
             var responseString = await response.Content.ReadAsStringAsync();
             var orderList = JsonConvert.DeserializeObject<List<OrderDtoDetail>>(responseString);
             Assert.True(response.IsSuccessStatusCode);
             Assert.Single(orderList);
        }

        [Fact]
        public async Task GetOrdersForCustomer_WhenAskOrdersAndNoOrdersFound_ThenBadRequest()
        {
            var response = await _client.GetAsync($"api/order/{testCustomerId}");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.False(response.IsSuccessStatusCode);
            Assert.Contains("No Orders Found for CustomerId", responseString);

        }





        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
