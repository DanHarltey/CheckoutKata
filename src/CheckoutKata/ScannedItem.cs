namespace CheckoutKata
{
    internal class ScannedItem : IScannedItem
    {
        public ScannedItem(Item item, uint quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public ScannedItem(IScannedItem scannedItem)
            : this(scannedItem.Item, scannedItem.Quantity)
        {
        }

        public Item Item { get; private init; }
        public uint Quantity { get; set; }
    }
}
