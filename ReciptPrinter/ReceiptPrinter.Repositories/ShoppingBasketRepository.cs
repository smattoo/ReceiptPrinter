using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Domain;

namespace ReceiptPrinter.Repositories
{
    public class ShoppingBasketRepository: IShoppingBasketRepository
    {
        private ShoppingBasketContext context;
        private bool disposed = false;

        public ShoppingBasketRepository(ShoppingBasketContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Add(ShoppingBasket shoppingBasketEntity)
        {
            this.context.ShoppingBasket.Add(shoppingBasketEntity);
        }

        public IEnumerable<ShoppingBasket> GetAllShoppingBaskets()
        {
            return this.context.ShoppingBasket.ToList();
        }

        public IEnumerable<ShoppingBasket> GetShoppingBasketByBasketNumber(int shoppingBasketNumber)
        {
            return this.context.ShoppingBasket.Where(s => s.ShoppingBasketNumber == shoppingBasketNumber).ToList();
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
