using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSystem.Domain
{
    public class ShoppingBasket : IShoppingBasket
    {
        [Key]
        public Guid ShoppingBasketRowId { get; set; }
        public int ShoppingBasketNumber { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}