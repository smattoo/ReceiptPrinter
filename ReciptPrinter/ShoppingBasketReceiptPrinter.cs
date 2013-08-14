using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BillingSystem.Domain;
using ReceiptPrinter.Repositories;


namespace ReciptPrinter
{
    public enum PrintStatus
    {
        ShoppingBasketEmpty,
        Sucess
    }

    public class ShoppingBasketReceiptPrinter
    {
        private readonly IShoppingBasketRepository shoppingBasket;
        private readonly ITaxCalculator taxCalculator;
        
        private readonly IRounder rounder;
        private readonly IPrinter printer;
        
        public ShoppingBasketReceiptPrinter(IShoppingBasketRepository shoppingBasket, ITaxCalculator taxCalculator, IRounder rounder, IPrinter printer)
        {
            this.shoppingBasket = shoppingBasket;
            this.taxCalculator = taxCalculator;
            this.rounder = rounder;
            this.printer = printer;
        }

        public PrintStatus PrintReceipt()
        {
            var shoppingBasketList = shoppingBasket.GetAllShoppingBaskets();

            if (!shoppingBasketList.Any())
                return PrintStatus.ShoppingBasketEmpty;

            var distinctBasketsNumbers =
                shoppingBasketList.OrderBy(s => s.ShoppingBasketNumber).Select(s => s.ShoppingBasketNumber).Distinct();
            
            
            foreach (var basketNumber in distinctBasketsNumbers)
            {
                Console.WriteLine(string.Format("-----Shopping Basket: {0}-----",basketNumber));
                var totalSalesTax = 0.0;
                var totalAmount = 0.0;

                var productsInOneBasket = shoppingBasketList.Where(s => s.ShoppingBasketNumber == basketNumber);
                foreach (var product in productsInOneBasket)
                {
                    var productDetail = product.ProductDetail;
                    var productSalesTax = taxCalculator.CalculateSalesTax(productDetail);
                    
                    totalSalesTax += productSalesTax;
                    var productTotal = productDetail.Price + productSalesTax;
                    
                    totalAmount += productTotal;
                    printer.Print(string.Format("{0} {1}: {2}", productDetail.Qty, productDetail.ProductName, productTotal));
                }

                printer.Print(string.Format("Sales Tax: {0}", totalSalesTax));
                printer.Print(string.Format("Total: {0}", totalAmount));

                Console.WriteLine();
            }
            return PrintStatus.Sucess;
        }
    }
}