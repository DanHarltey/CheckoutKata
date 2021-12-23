namespace CheckoutKata
{
    public interface IScannedItem
    {
        Item Item { get; }
        uint Quantity { get; }
    }
}