namespace CheckoutKata
{
    public interface ICheckout
    {
        IEnumerable<IScannedItem> ScannedItems { get; }
        void Scan(string sku);
    }
}
