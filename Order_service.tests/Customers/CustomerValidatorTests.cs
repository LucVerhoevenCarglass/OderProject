
using System;
using Order_domain.tests.Customers;
using Order_service.Customers;
using Xunit;

namespace Order_service.tests.Customers
{
    public class CustomerValidatorTests
    {
        [Fact]
        public void isValidForCreation_happyPath()
        {
            Assert.True(new CustomerValidator()
                .IsValidForCreation(CustomerTestBuilder.ACustomer()
                                                       .Build()));
        }

        [Fact]
        public void isValidForCreation_givenAnId_thenNotValidForCreation()
        {
            Assert.False(new CustomerValidator()
                .IsValidForCreation(CustomerTestBuilder.ACustomer()
                                                       .WithId(Guid.NewGuid())
                                                       .Build()));
        }

        /**
         * To have this code properly tested,
         * we should create a test for each individual empty or null value.
         * However, since this is not an application intended for production, we didn't.
         * Check the ItemValidatorTest for a better example.
         */
        [Fact]
        public void isValidForCreation_givenSomeMissingValues_thenNotValidForCreation()
        {
            Assert.False(new CustomerValidator()
                .IsValidForCreation(CustomerTestBuilder.ACustomer()
                    .WithId(Guid.NewGuid())
                    .WithFirstname("")
                    .WithLastname(null)
                    .WithEmail(null)
                    .Build()));
        }
    }
}
