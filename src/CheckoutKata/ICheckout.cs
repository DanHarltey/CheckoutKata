namespace CheckoutKata
{
    public interface ICheckout
    {
        IEnumerable<IScannedItem> ScannedItems { get; }
        decimal TotalPrice { get; }
        void Scan(string sku);
    }
}
