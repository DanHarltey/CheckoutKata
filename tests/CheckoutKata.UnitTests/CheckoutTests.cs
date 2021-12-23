using System;
using Xunit;

namespace CheckoutKata.UnitTests
{
    public class CheckoutTests
    {
        [Fact]
        public void WhenSkuNullThenThrowArgumentNullException()
        {
            // Arrange
            const string? Sku = null;
            var checkout = new Checkout(Array.Empty<Item>());

            // Act
            var scan = () => checkout.Scan(Sku!);

            // Assert
            Assert.Throws<ArgumentNullException>(scan);
        }

        [Fact]
        public void WhenInvalidSkuScannedThenThrowArgumentException()
        {
            // Arrange
            const string Sku = "A99";
            var checkout = new Checkout(Array.Empty<Item>());

            // Act
            var scan = () => checkout.Scan(Sku!);

            // Assert
            Assert.Throws<ArgumentException>(scan);
        }

        [Fact]
        public void WhenSkuScannedThenItIsAddedToCollection()
        {
            // Arrange
            const string Sku = "A99";
            const decimal Price = 1m;

            var checkout = new Checkout(new[] { new Item(Sku, Price) });

            // Act
            checkout.Scan(Sku);

            // Assert
            var item = Assert.Single(checkout.ScannedItems);
            Assert.NotNull(item);
            Assert.Equal(Sku, item.Sku);
            Assert.Equal(Price, item.UnitPrice);
        }
    }
}

