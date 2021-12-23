namespace CheckoutKata
{
    public static class CheckoutFactory
    {


        public static ICheckout Create()
        {
            var itemsQuery = new ItemsQuery();
            var items = itemsQuery.QueryItems();

            return new Checkout(items);
        }
    }
}