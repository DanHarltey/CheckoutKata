using System;
using System.Linq;
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
            ICheckout checkout = new Checkout(Array.Empty<Item>());

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
            ICheckout checkout = new Checkout(Array.Empty<Item>());

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

            ICheckout checkout = new Checkout(new[] { new Item(Sku, Price) });

            // Act
            checkout.Scan(Sku);

            // Assert
            var item = Assert.Single(checkout.ScannedItems);
            Assert.NotNull(item);
            Assert.Equal(Sku, item.Item.Sku);
            Assert.Equal(Price, item.Item.UnitPrice);
            Assert.Equal(1u, item.Quantity);
        }

        [Fact]
        public void WhenSkuScannedTwiceThenItHasTwoQuantity()
        {
            // Arrange
            const string Sku = "A99";
            const decimal Price = 1m;

            ICheckout checkout = new Checkout(new[] { new Item(Sku, Price) });

            // Act
            checkout.Scan(Sku);
            checkout.Scan(Sku);

            // Assert
            var item = Assert.Single(checkout.ScannedItems);
            Assert.NotNull(item);
            Assert.Equal(Sku, item.Item.Sku);
            Assert.Equal(Price, item.Item.UnitPrice);
            Assert.Equal(2u, item.Quantity);
        }

        [Fact]
        public void WhenSkuScannedOutOfOrderThenQuantityAdded()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            const string Sku2 = "B15";
            const decimal Sku2Price = 500m;

            ICheckout checkout = new Checkout(new Item[]
            {
                new Item(Sku1, Sku1Price),
                new Item(Sku2, Sku2Price)
            });

            // Act
            checkout.Scan(Sku1);
            checkout.Scan(Sku2);
            checkout.Scan(Sku1);

            // Assert
            var scannedItems = checkout.ScannedItems.ToList();
            Assert.Equal(2, scannedItems.Count);

            var sku1Item = scannedItems.Single(x => x.Item.Sku == Sku1);
            Assert.NotNull(sku1Item);
            Assert.Equal(Sku1, sku1Item.Item.Sku);
            Assert.Equal(Sku1Price, sku1Item.Item.UnitPrice);
            Assert.Equal(2u, sku1Item.Quantity);

            var sku2Item = scannedItems.Single(x => x.Item.Sku == Sku2);
            Assert.NotNull(sku2Item);
            Assert.Equal(Sku2, sku2Item.Item.Sku);
            Assert.Equal(Sku2Price, sku2Item.Item.UnitPrice);
            Assert.Equal(1u, sku2Item.Quantity);
        }

        [Fact]
        public void WhenSkuScannedThenTotalPriceIsCalculated()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            const string Sku2 = "B15";
            const decimal Sku2Price = 500m;

            ICheckout checkout = new Checkout(new Item[]
            {
                new Item(Sku1, Sku1Price),
                new Item(Sku2, Sku2Price)
            });

            // Act
            checkout.Scan(Sku1);
            checkout.Scan(Sku2);
            checkout.Scan(Sku1);

            // Assert
            var totalPrice = checkout.TotalPrice;
            Assert.Equal(502, totalPrice);
        }
    }
}

