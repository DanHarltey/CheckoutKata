using CheckoutKata.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CheckoutKata.UnitTests.Pricing
{
    public class PricingServiceTests
    {
        [Fact]
        public void WhenZeroItemsThenZeroIsReturned()
        {
            // Arrange
            var checkout = new Checkout(Array.Empty<Item>());
            IPricingService pricingService = new PricingService(new[] { new UnitPricing() });

            // Act
            var price = pricingService.CalculatePrice(checkout);

            // Assert
            Assert.Equal(0m, price);
        }

        [Fact]
        public void WhenSingleItemsThenReturnedTotalUnitPrice()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 1m;

            const string Sku2 = "B15";
            const decimal Sku2Price = 500m;

            var checkout = new Checkout(new Item[] { new(Sku1, Sku1Price), new(Sku2, Sku2Price) });
            checkout.Scan(Sku1);
            checkout.Scan(Sku1);
            checkout.Scan(Sku2);

            IPricingService pricingService = new PricingService(new[] { new UnitPricing() });

            // Act
            var price = pricingService.CalculatePrice(checkout);

            // Assert
            Assert.Equal(502m, price);
        }

        [Fact]
        public void WhenMulibuysAvailableThenReturnPrice()
        {
            // Arrange
            const string Sku1 = "A99";
            const decimal Sku1Price = 0.50m;

            const string Sku2 = "B15";
            const decimal Sku2Price = 0.30m;

            var checkout = new Checkout(new Item[] { new(Sku1, Sku1Price), new(Sku2, Sku2Price) });

            for (int i = 0; i < 7; i++)
            {
                checkout.Scan(Sku1);
            }

            checkout.Scan(Sku2);

            // Act
            IPricingService pricingService = new PricingService(new IPricingStrategy[]
            {
                new MultibuyOffer(Sku1, 3, 1.30m),
                new UnitPricing()
            });

            var price = pricingService.CalculatePrice(checkout);

            // Assert
            Assert.Equal(3.4m, price);
        }
    }
}
