using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BillingSystem.Domain
{
    public interface IShoppingBasket
    {
        int ShoppingBasketNumber { get; set; }
        ProductDetail ProductDetail { get; set; }
    }
}