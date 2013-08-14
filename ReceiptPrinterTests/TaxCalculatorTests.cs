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
        private Mock<IRounder> mockRounder;

        [SetUp]
        public void Setup()
        {
            mockRounder = new Mock<IRounder>(MockBehavior.Default);
            taxCalculator = new TaxCalculator(mockRounder.Object);
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

        [Test]
        public void should_round_total_sales_tax_for_a_product()
        {
            var nonExemptProduct = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Other);
            mockRounder.Setup(m => m.Round(It.IsAny<double>())).Returns(It.IsAny<double>);

            taxCalculator.CalculateSalesTax(nonExemptProduct.ProductDetail);

            mockRounder.Verify(m => m.Round(It.IsAny<double>()), Times.Exactly(1));
            mockRounder.VerifyAll();
        }

        [Test]
        public void should_calulate_total_sales_tax_for_non_imported_exempted_products()
        {
            
            //setup
            var nonImportedBook = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Books && t.ProductDetail.IsImported == false);
            
            var salesTax = taxCalculator.CalculateSalesTaxForProduct(nonImportedBook.ProductDetail);
            var importDuty = taxCalculator.CalculateImportDutyForProduct(nonImportedBook.ProductDetail);

            var productSalesTax = salesTax + importDuty;
            var expectedSalesTax = mockRounder.Object.Round(productSalesTax);
           
            //act
            var actualSalesTax = taxCalculator.CalculateSalesTax(nonImportedBook.ProductDetail);

            //verify
            Assert.That(actualSalesTax, Is.EqualTo(actualSalesTax));
        }

        [Test]
        public void should_calulate_total_sales_tax_for_non_imported_non_exempted_products()
        {
            //setup
            //TODO: is there a better way to do this?
            taxCalculator = new TaxCalculator(new Rounder());
            var nonExempted = mockShoppingBasketRepository.Object.GetAllShoppingBaskets()
                .FirstOrDefault(t => t.ProductDetail.ProductType == ProductType.Other && t.ProductDetail.IsImported == false);

            var salesTax = taxCalculator.CalculateSalesTaxForProduct(nonExempted.ProductDetail);
            var importDuty = taxCalculator.CalculateImportDutyForProduct(nonExempted.ProductDetail);

            var productSalesTax = salesTax + importDuty;
            var expectedSalesTax = new Rounder().Round(productSalesTax);

            //act
            var actualSalesTax = taxCalculator.CalculateSalesTax(nonExempted.ProductDetail);

            //verify
            Assert.That(actualSalesTax, Is.EqualTo(expectedSalesTax));
        }
    }
}
