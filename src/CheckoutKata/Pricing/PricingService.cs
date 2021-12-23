namespace CheckoutKata.Pricing
{
    public class PricingService : IPricingService
    {
        private readonly IEnumerable<IPricingStrategy> _pricingStrategies;

        internal PricingService(IEnumerable<IPricingStrategy> pricingStrategies)
            => _pricingStrategies = pricingStrategies;

        public decimal CalculatePrice(ICheckout checkout)
        {
            var scannedItems = checkout.ScannedItems
                .Select(x => new ScannedItem(x))
                .ToList();

            var totalPrice = 0m;

            foreach (var strategy in _pricingStrategies)
            {
                totalPrice += strategy.Calculate(scannedItems);
            }

            return totalPrice;
        }
    }
}
