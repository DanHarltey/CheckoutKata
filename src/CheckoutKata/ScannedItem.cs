namespace CheckoutKata
{
    internal class ScannedItem : IScannedItem
    {
        public ScannedItem(Item item, uint quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public Item Item { get; private init; }
        public uint Quantity { get; set; }

        public decimal TotalUnitPrice => Item.UnitPrice * Quantity;
    }
}
