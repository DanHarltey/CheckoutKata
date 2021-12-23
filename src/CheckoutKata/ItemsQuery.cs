namespace CheckoutKata
{
    internal class ItemsQuery
    {
        private static readonly Item[] items = new Item[]
        {
            new("A99", 0.50m),
            new("B15", 0.50m),
            new("C40", 0.50m)
        };

        public List<Item> QueryItems() => items.ToList();
    }
}
