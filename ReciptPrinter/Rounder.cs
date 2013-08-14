using System;

namespace ReciptPrinter
{
    public class Rounder : IRounder
    {
        public Rounder()
        {

        }

        public double Round(double productTotalSalesTax)
        {
            return  Math.Round(((Math.Round(productTotalSalesTax / 0.05)) * 0.05),2);
        }
    }
}