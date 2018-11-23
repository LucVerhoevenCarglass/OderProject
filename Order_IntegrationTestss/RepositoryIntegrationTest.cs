using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Order_api;
using Order_domain;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Order_service.Data;
using Xunit;

namespace Order_IntegrationTests
{
    public class RepositoryIntegrationTest : IDisposable
    {
        private readonly Repository<Customer> _repository;
        private readonly OrderContext _context;
        private readonly IDbContextTransaction _transaction;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public RepositoryIntegrationTest()
        {
            
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();

            _context = _server.Host.Services.GetService<OrderContext>();

            _repository = new CustomerRepository(_context);
            _transaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _transaction.Rollback();
            //_repository.Reset();
        }

        [Fact]
        public void Save()
        {
            var customerToSave = CustomerTestBuilder.ACustomer().Build();

            var savedCustomer = _repository.Save(customerToSave);

            Assert.NotEqual(Guid.Empty, savedCustomer.Id);
            Assert.Equal(savedCustomer, _repository.Get(savedCustomer.Id));
        }

        [Fact]
        public void Update()
        {
            var customerToSave = CustomerTestBuilder.ACustomer()
                .WithFirstname("Jo")
                .WithLastname("Jorissen")
                .Build();

            var count = _repository.GetAll().Count;
            var savedCustomer = _repository.Save(customerToSave);
            var updatedCustomer = _repository.Update(CustomerTestBuilder.ACustomer()
                    .WithId(savedCustomer.Id)
                    .WithFirstname("Joske")
                    .WithLastname("Jorissen")
                    .Build());

            Assert.NotEqual(Guid.Empty, savedCustomer.Id);
            Assert.Equal(savedCustomer.Id, updatedCustomer.Id);
            Assert.Equal("Joske", updatedCustomer.FirstName);
            Assert.Equal("Jorissen", updatedCustomer.LastName);

            Assert.Equal(count+1,_repository.GetAll().Count);
        }

        [Fact]
        public void Get()
        {
            var savedCustomer = _repository.Save(CustomerTestBuilder.ACustomer().Build());

            var actualCustomer = _repository.Get(savedCustomer.Id);

            Assert.Equal(actualCustomer, savedCustomer);
        }

        [Fact]
        public void GetAll()
        {
            var customer1 = _repository.Save(CustomerTestBuilder.ACustomer().Build());
            var customer2 = _repository.Save(CustomerTestBuilder.ACustomer().Build());

            var customers = _repository.GetAll();

            //Assert.IsType<Customer>(_repository.get)
            Assert.Contains(customer1, customers);
            Assert.Contains(customer2, customers);
        }
    }
}
