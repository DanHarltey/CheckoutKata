namespace CheckoutKata.Pricing
{
    internal class UnitPricing : IPricingStrategy
    {
        public decimal Calculate(IEnumerable<ScannedItem> scannedItems)
        {
            var total = 0m;

            foreach (var item in scannedItems)
            {
                total += item.Item.UnitPrice * item.Quantity;
                item.Quantity = 0;
            }

            return total;
        }
    }
}
