using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSystem.Domain
{
    public class ProductDetail
    {
        public int Qty { get; set; }
        public double Price { get; set; }
        public bool IsImported { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductName { get; set; }
    }
}