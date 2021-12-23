using CheckoutKata.Pricing;
using Xunit;

namespace CheckoutKata.UnitTests.Pricing
{
    public class MultibuyOfferTests
    {
        [Fact]
        public void WhenSkuHasOfferThenOfferIsApplied()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            IPricingStrategy multibuyOffer = new MultibuyOffer(Sku1, 3, 2m);

            var scannedItem = new ScannedItem(
                new Item(Sku1, Sku1Price),
                7);

            // Act
            var totalPrice = multibuyOffer.Calculate(new[] { scannedItem });

            // Assert
            Assert.Equal(4m, totalPrice);
            Assert.Equal(1u, scannedItem.Quantity);
        }

        [Fact]
        public void WhenItemHasZeroQuantityThenNoOffersApplied()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            IPricingStrategy multibuyOffer = new MultibuyOffer(Sku1, 3, 2m);

            var scannedItem = new ScannedItem(
                new Item(Sku1, Sku1Price),
                0);

            // Act
            var totalPrice = multibuyOffer.Calculate(new[] { scannedItem });

            // Assert
            Assert.Equal(0m, totalPrice);
            Assert.Equal(0u, scannedItem.Quantity);
        }

        [Fact]
        public void WhenItemNotInCollectionThenZeroIsReturned()
        {
            // Arrange
            const string Sku1 = "A99";

            IPricingStrategy multibuyOffer = new MultibuyOffer(Sku1, 3, 2m);

            var scannedItem = new ScannedItem(
                new Item("NotSku1", 99m),
                5);

            // Act
            var totalPrice = multibuyOffer.Calculate(new[] { scannedItem });

            // Assert
            Assert.Equal(0m, totalPrice);
            Assert.Equal(5u, scannedItem.Quantity);
        }
    }
}
