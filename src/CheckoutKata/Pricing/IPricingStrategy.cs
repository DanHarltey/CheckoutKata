namespace CheckoutKata.Pricing
{
    internal interface IPricingStrategy
    {
        decimal Calculate(IEnumerable<ScannedItem> scannedItems);
    }
}
