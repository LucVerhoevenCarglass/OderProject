using System;
using Order_domain.Items;
using Xunit;

namespace Order_domain.tests.Items
{
    public class ItemTest
    {
        [Fact]
        public void DecrementStock()
        {
            Item item = ItemTestBuilder.AnItem()
                .WithAmountOfStock(10)
                .Build();

            item.DecrementStock(8);

            Assert.Equal(2, item.AmountOfStock);
        }

        [Fact]
        public void DecrementStock_givenHigherAmountToDecrementThanActualRemainingStock_thenThrowException()
        {
            Guid itemId = Guid.NewGuid();
            Item item = ItemTestBuilder.AnItem()
                .WithId(itemId)
                .WithAmountOfStock(7)
                .Build();
            
            Exception ex = Assert.Throws<Exception>(() => item.DecrementStock(8));
            Assert.Equal("Decrementing the stock amount of an item " + itemId.ToString("N") + " below 0 is not allowed", ex.Message);
        }

        [Fact]
        public void GetStockUrgency_givenAmountOfStockLowerThan5_thenLowStockUrgency()
        {
            Item item = ItemTestBuilder.AnItem().WithAmountOfStock(4).Build();

            Assert.Equal(Item.StockUrgency.STOCK_LOW, item.GetStockUrgency());
        }

        [Fact]
        public void getStockUrgency_givenAmountOfStockLowerThan10_thenMediumStockUrgency()
        {
            Item item = ItemTestBuilder.AnItem().WithAmountOfStock(7).Build();

            Assert.Equal(Item.StockUrgency.STOCK_MEDIUM, item.GetStockUrgency());
        }

        [Fact]
        public void getStockUrgency_givenAmountOfStockEqualTo10_thenHighStockUrgency()
        {
            Item item = ItemTestBuilder.AnItem().WithAmountOfStock(10).Build();

            Assert.Equal(Item.StockUrgency.STOCK_HIGH, item.GetStockUrgency());
        }

        [Fact]
        public void getStockUrgency_givenAmountOfStockHigherThan10_thenHighStockUrgency()
        {
            Item item = ItemTestBuilder.AnItem().WithAmountOfStock(11).Build();

            Assert.Equal(Item.StockUrgency.STOCK_HIGH, item.GetStockUrgency());
        }
    }
}