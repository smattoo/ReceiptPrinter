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
    public class TaxCalculatorTests
    {
        private TaxCalculator taxCalculator;
        private Mock<IShoppingBasketRepository> mockShoppingBasketRepository;

        [SetUp]
        public void Setup()
        {
            taxCalculator = new TaxCalculator();
            mockShoppingBasketRepository = new Mock<IShoppingBasketRepository>();
            mockShoppingBasketRepository.Setup(rep => rep.GetAllShoppingBaskets()).Returns(FakeRepository.GetShoppingBaskets());
        }

        [Test]
        public void sales_tax_on_books_should_be_zero()
        {

            //setup
            var book = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Books);

            //act
            var salesTax = taxCalculator.CalculateSalesTaxForProduct(book.ProductDetail);

            //verify
            Assert.That(salesTax, Is.EqualTo(0));
        }

        [Test]
        public void sales_tax_on_food_should_be_zero()
        {

            //setup
            var food = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Food);

            //act
            var salesTax = taxCalculator.CalculateSalesTaxForProduct(food.ProductDetail);

            //verify
            Assert.That(salesTax, Is.EqualTo(0));
        }

        [Test]
        public void sales_tax_on_medical_should_be_zero()
        {

            //setup
            var medical = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Medical);

            //act
            var salesTax = taxCalculator.CalculateSalesTaxForProduct(medical.ProductDetail);

            //verify
            Assert.That(salesTax, Is.EqualTo(0));
        }

        [Test]
        public void sales_tax_on_non_exempt_should_be_10_percent()
        {

            //setup
            var nonExemptProduct = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Other);

            //act
            var salesTax = taxCalculator.CalculateSalesTaxForProduct(nonExemptProduct.ProductDetail);

            var expectedSalesTax = 0.10 * nonExemptProduct.ProductDetail.Price * nonExemptProduct.ProductDetail.Qty;
         
            //verify
            Assert.That(salesTax, Is.EqualTo(expectedSalesTax));
        }

        [Test]
        public void import_duty_on_imported_products_should_be_5_percent()
        {
            //setup
            var importedProduct = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.IsImported == true);

            //act
            var importDuty = taxCalculator.CalculateImportDutyForProduct(importedProduct.ProductDetail);

            //verify
            Assert.That(importDuty, Is.EqualTo(0.05 * importedProduct.ProductDetail.Price));
        }


        [Test]
        public void import_duty_on_non_imported_products_should_be_0()
        {
            //setup
            var nonImportedProduct = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.IsImported == false);

            //act
            var importDuty = taxCalculator.CalculateImportDutyForProduct(nonImportedProduct.ProductDetail);

            //verify
            Assert.That(importDuty, Is.EqualTo(0));
        }
    }
}
