using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BillingSystem.Domain;
using ReceiptPrinter.Repositories;


namespace ReciptPrinter
{
    public class ShoppingBasketReceiptPrinter
    {
        private readonly IShoppingBasketRepository shoppingBasket;
        private readonly ITaxCalculator taxCalculator;

        public ShoppingBasketReceiptPrinter(IShoppingBasketRepository shoppingBasket, ITaxCalculator taxCalculator)
        {
            this.shoppingBasket = shoppingBasket;
            this.taxCalculator = taxCalculator;
        }

        public PrintStatus PrintReceipt()
        {
            var shoppingBasketList = shoppingBasket.GetAllShoppingBaskets();

            if (!shoppingBasketList.Any())
                return PrintStatus.ShoppingCartEmpty;

            var distinctBasketsNumbers = shoppingBasketList.Select(s => s.ShoppingBasketNumber).Distinct();

            foreach (var basketNumber in distinctBasketsNumbers)
            {
                var totalSalesTax = 0.0;
                var totalAmount = 0.0;

                var productsInOneBasket = shoppingBasketList.Where(s => s.ShoppingBasketNumber == basketNumber);
                foreach (var product in productsInOneBasket)
                {
                    var productDetail = product.ProductDetail;
                    var productSalesTax = taxCalculator.CalculateSalesTaxForProduct(productDetail);
                    var productImportDuty = taxCalculator.CalculateImportDutyForProduct(productDetail);

                    var productTotalSalesTax = productSalesTax + productImportDuty;
                    var roundedProductTotalSalesTax = (Math.Round(productTotalSalesTax/0.05))* 0.05;

                    totalSalesTax += roundedProductTotalSalesTax;
                    var productTotal = productDetail.Price + roundedProductTotalSalesTax;
                    
                    totalAmount += productTotal;
                    Console.WriteLine(string.Format("{0} {1}: {2}", productDetail.Qty, productDetail.ProductType, productTotal));
                }
                Console.WriteLine(string.Format("Sales Tax: {0}", totalSalesTax));
                Console.WriteLine(string.Format("Total: {0}", totalAmount));
            }
            return PrintStatus.Sucess;
        }

        
        public enum PrintStatus
        {
            ShoppingCartEmpty,
            Sucess
        }
    }
}