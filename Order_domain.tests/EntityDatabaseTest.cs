using System.Linq;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Xunit;

namespace Order_domain.tests
{
    public class EntityDatabaseTest
    {
        [Fact]
        public void Populate_GetAll()
        {
            EntityDatabase<Customer> database = new CustomerDatabase();
            var customer1 = CustomerTestBuilder.ACustomer().Build();
            var customer2 = CustomerTestBuilder.ACustomer().Build();
            var customer3 = CustomerTestBuilder.ACustomer().Build();

            customer1.GenerateId();
            customer2.GenerateId();
            customer3.GenerateId();

            database.Populate(customer1, customer2, customer3);

            var customers = database.GetAll();
            
            Assert.Equal(3, customers.Count);
            Assert.True(customers.ContainsValue(customer1));
            Assert.True(customers.ContainsValue(customer2));
            Assert.True(customers.ContainsValue(customer3));
        }

        [Fact]
        public void Save_GivenDifferentId_ThenStoreBoth()
        {
            EntityDatabase<Customer> database = new CustomerDatabase();

            var customer = CustomerTestBuilder.ACustomer().Build();
            customer.GenerateId();
            database.Populate(customer);

            var customer2 = CustomerTestBuilder.ACustomer().Build();
            customer2.GenerateId();
            database.Save(customer2);

            var customers = database.GetAll();

            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public void Save_GivenSameId_ThenUpdateExisting()
        {
            EntityDatabase<Customer> database = new CustomerDatabase();
            var customer = CustomerTestBuilder.ACustomer()
                .WithFirstname("OriginalName")
                .Build();
            customer.GenerateId();
            database.Populate(customer);

            var customer2 = CustomerTestBuilder.ACustomer()
                .WithFirstname("NewName")
                .WithId(customer.Id)
                .Build();
            database.Save(customer2);

            var customers = database.GetAll();

            Assert.Single(customers);
            Assert.Equal("NewName", customers.Single(x => x.Value.Id == customer.Id).Value.FirstName);
        }
    }
}
