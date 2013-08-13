using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using BillingSystem.Domain;
using Moq;
using NUnit.Framework;
using ReceiptPrinter.Repositories;
using ReciptPrinter;
using Shouldly;

namespace ReceiptPrinterTests
{
    [TestFixture]
    public class ReceiptPrinterTests
    {

        private Mock<IShoppingBasketRepository> mockShoppingBasketRepository;
        private TaxCalculator taxCalculator;
        private ShoppingBasketReceiptPrinter shoppingBasketReceiptPrinter;

        [SetUp]
        public void Setup()
        {
            mockShoppingBasketRepository = new Mock<IShoppingBasketRepository>();
            mockShoppingBasketRepository.Setup(rep => rep.GetAllShoppingBaskets()).Returns(FakeRepository.GetShoppingBaskets());
            taxCalculator = new TaxCalculator();
            //shoppingBasketReceiptPrinter = new ShoppingBasketReceiptPrinter(mockShoppingBasketRepository.Object,taxCalculator);
        }


       // [Test]
       // public void sales_tax_on_books_should_be_zero()
       // {
       //     //act
       //     shoppingBasketReceiptPrinter.PrintReceipt();
            
           
       //     //verify
       //     //var salesTax = taxCalculator.CalculateSalesTaxForProduct(product);
       ////     Assert.That(salesTax,Is.EqualTo(0));
       // }

        //[Test]
        //public void sales_tax_on_food_should_be_zero()
        //{
        //    //setup
        //    var product = mockProductDetail.Object;
        //    product.IsImported = false;
        //    product.ProductType = ProductType.Food;
        //    product.Price = It.IsInRange(1, double.MaxValue, Range.Inclusive);
        //    product.Qty = It.IsInRange(1, int.MaxValue, Range.Inclusive);

        //    var salesTax = taxCalculator.CalculateSalesTaxForProduct(product);
        //    Assert.That(salesTax, Is.EqualTo(0));
        //}

        //[Test]
        //public void sales_tax_on_medical_should_be_zero()
        //{
        //    //setup
        //    var product = mockProductDetail.Object;
        //    product.IsImported = false;
        //    product.ProductType = ProductType.Medical;
        //    product.Price = It.IsInRange(1, double.MaxValue, Range.Inclusive);
        //    product.Qty = It.IsInRange(1, int.MaxValue, Range.Inclusive);

        //    var salesTax = taxCalculator.CalculateSalesTaxForProduct(product);
        //    Assert.That(salesTax, Is.EqualTo(0));
        //}

        //[Test]
        //public void sales_tax_on_other_should_be_10_percent()
        //{
        //    //setup
        //    var product = mockProductDetail.Object;
        //    product.IsImported = false;
        //    product.ProductType = ProductType.Other;
        //    product.Price = 1;
        //    product.Qty = 1;

        //    var salesTax = taxCalculator.CalculateSalesTaxForProduct(product);
        //    Assert.That(salesTax, !Is.EqualTo(0));
        //}

        //[Test]
        //public void should_not_print_if_shopping_basket_empty()
        //{   
        //    var shoppingBasket = mockShoppingBasket.Object;
        //    shoppingBasket.Products = new List<IProductDetail>();
        //    shoppingBasketReceiptPrinter = new ShoppingBasketReceiptPrinter(shoppingBasket, taxCalculator);
        //    var status =  shoppingBasketReceiptPrinter.PrintReceipt();
        //    status.ShouldBe(ShoppingBasketReceiptPrinter.PrintStatus.ShoppingCartEmpty);
        //}



    }
}
