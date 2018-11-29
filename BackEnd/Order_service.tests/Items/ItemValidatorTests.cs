using System;
using Order_domain.Items.Prices;
using Order_domain.tests.Items;
using Order_service.Items;
using Xunit;

namespace Order_service.tests.Items
{
    public class ItemValidatorTests
    {
        [Fact]
        public void isValidForCreation_happyPath()
        {
            Assert.True(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().Build()));
        }

        [Fact]
        public void isValidForCreation_givenId_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithId(Guid.NewGuid()).Build()));
        }

        [Fact]
        public void isValidForCreation_givenEmptyName_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithName(string.Empty).Build()));
        }

        [Fact]
        public void isValidForCreation_givenNullName_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithName(null).Build()));
        }

        [Fact]
        public void isValidForCreation_givenEmptyDescription_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithDescription(string.Empty).Build()));
        }

        [Fact]
        public void isValidForCreation_givenNullDescription_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithDescription(null).Build()));
        }

        [Fact]
        public void isValidForCreation_givenZeroPrice_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithPrice(Price.Create(new decimal(0))).Build()));
        }

        [Fact]
        public void isValidForCreation_givenNegativePrice_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithPrice(Price.Create(new decimal(-1))).Build()));
        }

        [Fact]
        public void isValidForCreation_givenNullPrice_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithPrice(null).Build()));
        }

        [Fact]
        public void isValidForCreation_givenNegativeStock_thenNotValidForCreation()
        {
            Assert.False(new ItemValidator().IsValidForCreation(ItemTestBuilder.AnItem().WithAmountOfStock(-1).Build()));
        }

        [Fact]
        public void isValidForUpdating_happyPath()
        {
            Assert.True(new ItemValidator().IsValidForUpdating(ItemTestBuilder.AnItem().WithId(Guid.NewGuid()).Build()));
        }

        [Fact]
        public void isValidForUpdating_givenNoId_thenNotValidForUpdating()
        {
            Assert.False(new ItemValidator().IsValidForUpdating(ItemTestBuilder.AnItem().Build()));
        }
    }
}
