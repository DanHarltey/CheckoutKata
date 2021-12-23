using CheckoutKata.Pricing;
using Xunit;

namespace CheckoutKata.UnitTests.Pricing
{
    public class UnitPricingTests
    {
        [Fact]
        public void WhenSkuHasOfferThenOfferIsApplied()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            const string Sku2 = "B15";
            const decimal Sku2Price = 500m;


            IPricingStrategy pricingStrategy = new UnitPricing();

            var scannedItem1 = new ScannedItem(
                new Item(Sku1, Sku1Price),
                7);

            var scannedItem2 = new ScannedItem(
                new Item(Sku2, Sku2Price),
                3);

            // Act
            var totalPrice = pricingStrategy.Calculate(new[] { scannedItem1, scannedItem2 });

            // Assert
            Assert.Equal(1507m, totalPrice);
            Assert.Equal(0u, scannedItem1.Quantity);
            Assert.Equal(0u, scannedItem2.Quantity);
        }

        [Fact]
        public void WhenItemHasZeroQuantityThenNoOffersApplied()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            IPricingStrategy pricingStrategy = new MultibuyOffer(Sku1, 3, 2m);

            var scannedItem = new ScannedItem(
                new Item(Sku1, Sku1Price),
                0);

            // Act
            var totalPrice = pricingStrategy.Calculate(new[] { scannedItem });

            // Assert
            Assert.Equal(0m, totalPrice);
            Assert.Equal(0u, scannedItem.Quantity);
        }
    }
}
