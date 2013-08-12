using System;
using System.Collections.Generic;
using BillingSystem.Domain;

namespace ReceiptPrinter.Repositories
{
    public interface IShoppingBasketRepository: IDisposable
    {
        void Add(ShoppingBasket shoppingBasketEntity);
        IEnumerable<ShoppingBasket> GetAllShoppingBaskets();
        IEnumerable<ShoppingBasket> GetShoppingBasketByBasketNumber(int shoppingBasketId);
        void Save();
    }
}