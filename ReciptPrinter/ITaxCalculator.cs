using BillingSystem.Domain;

namespace ReciptPrinter
{
    public interface ITaxCalculator
    {
        double CalculateSalesTaxForProduct(ProductDetail productDetail);
        double CalculateImportDutyForProduct(ProductDetail productDetail);
    }
}