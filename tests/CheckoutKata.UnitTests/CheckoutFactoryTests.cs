using Xunit;

namespace CheckoutKata.UnitTests
{
    public class CheckoutFactoryTests
    {
        [Fact]
        public void WhenCreateThenReturnsCheckout()
        {
            // Act
            var checkout = CheckoutFactory.Create();

            // Assert
            Assert.NotNull(checkout);
        }

        [Fact]
        public void WhenCreateThenReturnsCheckoutWithItems()
        {
            // Act
            const string Sku = "A99";
            var checkout = CheckoutFactory.Create();

            // Act, Assert, this would throw if default items where not added
            checkout.Scan(Sku);
        }
    }
}