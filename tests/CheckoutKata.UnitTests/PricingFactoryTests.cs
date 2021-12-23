using Xunit;

namespace CheckoutKata.UnitTests
{
    public class PricingFactoryTests
    {
        [Fact]
        public void CreateThenReturnsPricingService()
        {
            // Act
            var pricingFactory = PricingFactory.Create();

            // Assert
            Assert.NotNull(pricingFactory);
        }

        [Fact]
        public void CreateReturnsPricingFactoryWithStrategy()
        {
            // Arrange
            const string Sku = "A99";
            var checkout = new Checkout(new[] { new Item(Sku, 1m) });
            checkout.Scan(Sku);

            // Act
            var pricingFactory = PricingFactory.Create();
            var price = pricingFactory.CalculatePrice(checkout);

            // Assert
            Assert.Equal(1m, price);
        }
    }
}
