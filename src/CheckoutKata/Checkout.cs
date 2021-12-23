namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly IReadOnlyDictionary<string, Item> _items;
        private readonly List<Item> _scannedItems;

        public Checkout(IEnumerable<Item> items)
        {
            _items = items.ToDictionary(x => x.Sku);
            _scannedItems = new List<Item>();
        }

        public IEnumerable<Item> ScannedItems => _scannedItems.AsReadOnly();

        public void Scan(string sku)
        {
            ArgumentNullException.ThrowIfNull(sku);

            if(!_items.TryGetValue(sku, out var item))
            {
                throw new ArgumentException("Could not find passed Sku", nameof(sku));
            }

            _scannedItems.Add(item);
        }
    }
}
