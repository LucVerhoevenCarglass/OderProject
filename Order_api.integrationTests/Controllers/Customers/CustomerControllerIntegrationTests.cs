using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Oder_infrastructure.Exceptions;
using Order_api.Controllers.Customers;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;
using Xunit;

namespace Order_api.integrationTests.Controllers.Customers
{
    public class CustomerControllerIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;

        public CustomerControllerIntegrationTests()
        {
            _client = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>())
                .CreateClient();
        }

        private CustomerDto CreateCustomerDto()
        {
            return CustomerDtoBuilder.CustomerDto()
                .WithFirstname("Bruce")
                .WithLastname("Wayne")
                .WithEmail(new EmailDto()
                    .WithLocalPart("brucy")
                    .WithDomain("bat.net")
                    .WithComplete("brucy@bat.net"))
                .WithPhoneNumber(new PhoneNumberDto()
                    .WithNumber("485212121")
                    .WithCountryCallingCode("+32"))
                .WithAddress(new AddressDto()
                    .WithStreetName("Secretstreet")
                    .WithHouseNumber("841")
                    .WithPostalCode("1238")
                    .WithCountry("Gotham"))
                .Build();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        [Fact]
        public async Task CreateCustomer()
        {
            CustomerDto customerToCreate = CreateCustomerDto();

            var content = JsonConvert.SerializeObject(customerToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/customers", stringContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var createdCustomer = JsonConvert.DeserializeObject<CustomerDto>(responseString);

            AssertCustomerIsEqualIgnoringId(customerToCreate, createdCustomer);
        }

        [Fact]
        public async Task CreateCustomer_givenCustomerNotValidForCreationBecauseOfMissingFirstName_thenErrorObjectReturnedByControllerExceptionHandler()
        {
            CustomerDto customerToCreate = CreateCustomerDto();
            customerToCreate.FirstName = string.Empty;

            var content = JsonConvert.SerializeObject(customerToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/customers", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ErrorDto>(responseString);

            Assert.NotNull(error);
            Assert.False(string.IsNullOrWhiteSpace(error.UniqueErrorId));
            Assert.Contains("Invalid Customer provided for creation. " + "Provided object: Customer{id=", error.Message);
        }

        [Fact]
        public async Task GetAllCustomers()
        {
            CustomerDto customerToCreate = CreateCustomerDto();

            var content = JsonConvert.SerializeObject(customerToCreate);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/customers", stringContent);
            await _client.PostAsync("/api/customers", stringContent);

            var response = await _client.GetAsync("/api/customers");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var allCustomers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(responseString);

            Assert.Equal(2, allCustomers.Count());
        }

        [Fact]
        public async Task GetAllCustomers_assertResultIsCorrectlyReturned()
        {
            CustomerDto customer = CreateCustomerDto();

            var content = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/customers", stringContent);

            var response = await _client.GetAsync("/api/customers");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var allCustomers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(responseString).ToList();

            Assert.Single(allCustomers);
            AssertCustomerIsEqualIgnoringId(customer, allCustomers.First());
        }

        [Fact]
        public async Task GetCustomer()
        {
            CustomerDto customer = CreateCustomerDto();

            var content = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResponse = await _client.PostAsync("/api/customers", stringContent);
            var postResponseString = await postResponse.Content.ReadAsStringAsync();
            var customerToFind = JsonConvert.DeserializeObject<CustomerDto>(postResponseString);

            var getResponse = await _client.GetAsync("/api/customers/" + customerToFind.Id);
            var getResponseString = await getResponse.Content.ReadAsStringAsync();
            var foundCustomer = JsonConvert.DeserializeObject<CustomerDto>(getResponseString);

            AssertCustomerIsEqualIgnoringId(customer, foundCustomer);
        }

        private void AssertCustomerIsEqualIgnoringId(CustomerDto customerToCreate, CustomerDto createdCustomer)
        {
            Assert.False(string.IsNullOrWhiteSpace(createdCustomer.Id));

            Assert.Equal(customerToCreate.FirstName, createdCustomer.FirstName);
            Assert.Equal(customerToCreate.LastName, createdCustomer.LastName);

            Assert.Equal(customerToCreate.Address.StreetName, createdCustomer.Address.StreetName);
            Assert.Equal(customerToCreate.Address.HouseNumber, createdCustomer.Address.HouseNumber);
            Assert.Equal(customerToCreate.Address.PostalCode, createdCustomer.Address.PostalCode);
            Assert.Equal(customerToCreate.Address.Country, createdCustomer.Address.Country);

            Assert.Equal(customerToCreate.Email.LocalPart, createdCustomer.Email.LocalPart);
            Assert.Equal(customerToCreate.Email.Domain, createdCustomer.Email.Domain);
            Assert.Equal(customerToCreate.Email.Complete, createdCustomer.Email.Complete);

            Assert.Equal(customerToCreate.PhoneNumber.Number, createdCustomer.PhoneNumber.Number);
            Assert.Equal(customerToCreate.PhoneNumber.CountryCallingCode, createdCustomer.PhoneNumber.CountryCallingCode);
        }
    }
}
