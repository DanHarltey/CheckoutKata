namespace CheckoutKata
{
    public record Item
    {
        public Item(string sku, decimal unitPrice)
        {
            Sku = sku;
            UnitPrice = unitPrice;
        }

        public string Sku { get; private init; }
        public decimal UnitPrice { get; private init; }
    }
}
