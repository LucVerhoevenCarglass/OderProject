using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain;
using Order.Domain.Customers;
using Order.Domain.Items;
using Xunit;

namespace Order.Domain.UnitTests.Items
{
    public class ItemTest
    {
        [Fact]
        public void GivenItemDatabase_WhenCreateNewItem_ThenNameRequired()
        {
            Item newItem = new Item()
            {
                Name = "",
                Description = "Description"
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newItem.CheckItemValues());
            Assert.Contains("is required", errorExeption.Message);
        }

        [Fact]
        public void GivenItemDatabase_WhenCreateNewItem_ThenDescriptionRequired()
        {
            Item newItem = new Item()
            {
                Name = "ProductId",
                Description = ""
            };
            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newItem.CheckItemValues());
            Assert.Contains("is required", errorExeption.Message);
        }
    }
}
