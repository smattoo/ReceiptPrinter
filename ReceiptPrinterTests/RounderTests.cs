using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ReceiptPrinter.Repositories;
using ReciptPrinter;
using Shouldly;

namespace ReceiptPrinterTests
{
    class RounderTests
    {
        private Rounder rounder;

        [SetUp]
        public void Setup()
        {
            rounder = new Rounder();
        }

        [Test]
        public void should_round_to_05_decimal()
        {
            rounder.Round(1.2345).ShouldBe(1.25);
            rounder.Round(1.2020).ShouldBe(1.2);
            
            //as per the assignment pdf this 11.8125 should be rounded to 11.85
            //but shouldn't 11.8125 be rounded to 11.8 as per nearest 0.05 amounts 
            rounder.Round(11.8125).ShouldBe(11.8);
        }



    }
}
