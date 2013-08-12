using System;

namespace BillingSystem.Domain
{
    public interface IProductDetail
    {
        int Qty { get; set; }
        double Price { get; set; }
        bool IsImported { get; set; }
        ProductType ProductType { get; set; }
        string ProductName { get; set; }
    }

}