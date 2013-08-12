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

            var  shoppingBasketReceiptPrinter = new ShoppingBasketReceiptPrinter(shoppingBasket,taxCalculator);
            shoppingBasketReceiptPrinter.PrintReceipt();

            Console.ReadKey();
        }
    }
}