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
        private Mock<ITaxCalculator> mockTaxCalculator;
        private Mock<IPrinter> mockPrinter;
        private ShoppingBasketReceiptPrinter shoppingBasketReceiptPrinter;

        [SetUp]
        public void Setup()
        {
            var mockRepo = new MockRepository(MockBehavior.Loose);
            mockTaxCalculator = mockRepo.Create<ITaxCalculator>();
            mockPrinter = mockRepo.Create<IPrinter>();
            mockShoppingBasketRepository = new Mock<IShoppingBasketRepository>();
            shoppingBasketReceiptPrinter = new ShoppingBasketReceiptPrinter(mockShoppingBasketRepository.Object, mockTaxCalculator.Object, mockPrinter.Object);
        }


        [Test]
        public void should_return_shopping_basket_emoty_if_shopping_basket_is_empty()
        {
            mockShoppingBasketRepository.Setup(rep => rep.GetAllShoppingBaskets()).Returns(new List<ShoppingBasket>());
            var status = shoppingBasketReceiptPrinter.PrintReceipt();
            status.ShouldBe(PrintStatus.ShoppingBasketEmpty);
        }

        [Test]
        public void shoud_calculate_sales_tax_and_import_duty_for_each_product_in_all_the_shopping_baskets()
        {   
            mockTaxCalculator.Setup(m => m.CalculateSalesTax(It.IsAny<ProductDetail>()))
                .Returns(It.IsAny<double>);
            
            mockShoppingBasketRepository.Setup(rep => rep.GetAllShoppingBaskets()).Returns(FakeRepository.GetShoppingBaskets());
            var totalProducts = FakeRepository.GetShoppingBaskets().Count();
            shoppingBasketReceiptPrinter.PrintReceipt();
            mockTaxCalculator.Verify(m => m.CalculateSalesTax(It.IsAny<ProductDetail>()), Times.Exactly(totalProducts));
            mockTaxCalculator.VerifyAll();
        }

       
        [Test]
        public void should_print_details_for_each_product_in_shoppingbasket()
        {
            mockPrinter.Setup(m => m.Print(It.IsAny<string>()));
            mockShoppingBasketRepository.Setup(rep => rep.GetAllShoppingBaskets()).Returns(FakeRepository.GetShoppingBaskets());
            var totalNumberOfProducts = FakeRepository.GetShoppingBaskets().Count();

            var totalPrintCommands = totalNumberOfProducts + 2 * FakeRepository.NumberOfUniqueBaskets;

            shoppingBasketReceiptPrinter.PrintReceipt();
            mockPrinter.Verify(m => m.Print(It.IsAny<string>()), Times.Exactly(totalPrintCommands));
            mockPrinter.VerifyAll();
        }
    }
}
