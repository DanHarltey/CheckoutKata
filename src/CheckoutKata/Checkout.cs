namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        private readonly IReadOnlyDictionary<string, Item> _items;
        private readonly Dictionary<Item, ScannedItem> _scannedItems;

        public Checkout(IEnumerable<Item> items)
        {
            _items = items.ToDictionary(x => x.Sku);
            _scannedItems = new();
        }

        public IEnumerable<IScannedItem> ScannedItems => _scannedItems.Values;

        public void Scan(string sku)
        {
            ArgumentNullException.ThrowIfNull(sku);

            if(!_items.TryGetValue(sku, out var item))
            {
                throw new ArgumentException("Could not find passed Sku", nameof(sku));
            }

            if (_scannedItems.TryGetValue(item, out var scannedItem))
            {
                ++scannedItem.Quantity;
            }
            else
            {
                _scannedItems.Add(item, new ScannedItem(item, 1));
            }
        }
    }
}
