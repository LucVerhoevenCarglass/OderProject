using System;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Xunit;

namespace Order_domain.tests
{
    public class RepositoryIntegrationTest : IDisposable
    {
        private readonly Repository<Customer, CustomerDatabase> _repository;

        public RepositoryIntegrationTest()
        {
            _repository = new CustomerRepository(new CustomerDatabase());
        }
        
        public void Dispose()
        {
            _repository.Reset();
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
            Assert.Single(_repository.GetAll());
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

            Assert.True(customers.ContainsValue(customer1));
            Assert.True(customers.ContainsValue(customer2));
        }
    }
}
