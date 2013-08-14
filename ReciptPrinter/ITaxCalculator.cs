using BillingSystem.Domain;

namespace ReciptPrinter
{
    public interface ITaxCalculator
    {
        double CalculateSalesTax(ProductDetail productDetail);
    }
}