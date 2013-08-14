using System;
using System.Collections.Generic;
using BillingSystem.Domain;

namespace ReciptPrinter
{
    public class TaxCalculator : ITaxCalculator
    {
        private const double salesTaxRate = 0.10;
        private const double importDutyRate = 0.05;

        public virtual double CalculateSalesTaxForProduct(ProductDetail productDetail)
        {
            double salesTax = 0;
            switch (productDetail.ProductType)
            {
                case ProductType.Other:
                    salesTax = salesTaxRate * productDetail.Price * productDetail.Qty;
                    break;
                case ProductType.Food:
                    salesTax = 0;
                    break;
                case ProductType.Medical:
                    salesTax = 0;
                    break;
                case ProductType.Books:
                    salesTax = 0;
                    break;
            }

            return salesTax;
        }

        public virtual double CalculateImportDutyForProduct(ProductDetail productDetail)
        {
            if (!productDetail.IsImported)
                return 0;
            else
            {
                var importDuty = importDutyRate * productDetail.Price * productDetail.Qty;
                return importDuty;
            }
        }
    }
}