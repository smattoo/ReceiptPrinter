﻿using System;
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
            var taxCalculator = (TaxCalculator) AutomaticFactory.GetMeOne(typeof(TaxCalculator));
            var printer = (Printer)AutomaticFactory.GetMeOne(typeof(Printer));
            var rounder = (Rounder)AutomaticFactory.GetMeOne(typeof(Rounder));

            var  shoppingBasketReceiptPrinter = new ShoppingBasketReceiptPrinter(shoppingBasket,taxCalculator,rounder,printer );
            shoppingBasketReceiptPrinter.PrintReceipt();

            Console.ReadKey();
        }
    }
}
