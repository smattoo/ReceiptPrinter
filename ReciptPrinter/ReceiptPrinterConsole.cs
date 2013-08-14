using System;
using System.CodeDom;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Domain;
using ReceiptPrinter.Repositories;

namespace ReciptPrinter
{
    internal class ReceiptPrinterConsole
    {
        private static void Main(string[] args)
        {
            var shoppingBasket = (ShoppingBasketRepository) AutomaticFactory.GetMeOne(typeof(ShoppingBasketRepository));
            var rounder = (Rounder)AutomaticFactory.GetMeOne(typeof(Rounder));
            var taxCalculator = (TaxCalculator) AutomaticFactory.GetMeOne(typeof(TaxCalculator));
            var printer = (Printer)AutomaticFactory.GetMeOne(typeof(Printer));
            

            var  shoppingBasketReceiptPrinter = new ShoppingBasketReceiptPrinter(shoppingBasket,taxCalculator,printer);
            shoppingBasketReceiptPrinter.PrintReceipt();

            Console.ReadKey();
        }
    }
}
