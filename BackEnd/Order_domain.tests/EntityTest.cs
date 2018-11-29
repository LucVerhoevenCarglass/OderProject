using System;
using Order_domain.tests.Customers;
using Xunit;

namespace Order_domain.tests
{
    public class EntityTest
    {
        [Fact]
        public void GenerateId_GivenCustomerWithoutId_WhenGeneratingId_ThenGenerateId()
        {
            Entity customer = CustomerTestBuilder.ACustomer().Build();
            customer.GenerateId();

            Assert.NotEqual(Guid.Empty, customer.Id);
        }

        [Fact]
        public void generateId_givenCustomerWithId_whenGeneratingId_thenThrowException()
        {
            var id = Guid.NewGuid();
            Entity customer = CustomerTestBuilder.ACustomer()
                .WithId(id)
                .Build();
            
            Exception ex = Assert.Throws<Exception>(() => customer.GenerateId());
            Assert.Equal("Generating an ID for a customer that already has an ID (" + id + ") is not allowed.", ex.Message);
        }
    }
}
