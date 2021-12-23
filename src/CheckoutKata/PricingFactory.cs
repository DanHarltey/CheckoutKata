using CheckoutKata;
using CheckoutKata.Pricing;

namespace CheckoutKata
{
    public static class PricingFactory
    {
        private static readonly IPricingStrategy[] DefaultPricingStrategies = new IPricingStrategy[]
         {
            new MultibuyOffer("A99", 3, 1.30m),
            new MultibuyOffer("B15", 2, 0.45m),
            new UnitPricing()
         };

        public static IPricingService Create() => new PricingService(DefaultPricingStrategies);
    }
}
