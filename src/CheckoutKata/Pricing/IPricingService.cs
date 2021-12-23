namespace CheckoutKata.Pricing
{
    public interface IPricingService
    {
        decimal CalculatePrice(ICheckout checkout);
    }
}