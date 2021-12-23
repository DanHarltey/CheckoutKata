namespace CheckoutKata.Pricing
{
    internal class MultibuyOffer : IPricingStrategy
    {
        public MultibuyOffer(string sku, uint quantityNeeded, decimal price)
        {
            Sku = sku;
            QuantityNeeded = quantityNeeded;
            Price = price;
        }

        public string Sku { get; private init; }
        public uint QuantityNeeded { get; private init; }
        public decimal Price { get; private init; }

        public decimal Calculate(IEnumerable<ScannedItem> scannedItems)
        {
            var availableItem = scannedItems.SingleOrDefault(x => x.Item.Sku == Sku);

            if (availableItem == null)
            {
                return 0m;
            }

            var multibuys = availableItem.Quantity / QuantityNeeded;

            availableItem.Quantity -= multibuys * QuantityNeeded;

            return multibuys * Price;
        }
    }
}
