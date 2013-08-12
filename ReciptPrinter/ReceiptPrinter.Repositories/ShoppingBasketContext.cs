using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BillingSystem.Domain;

namespace ReceiptPrinter.Repositories
{
    public class ShoppingBasketContext : DbContext
    {
        public DbSet<ShoppingBasket> ShoppingBasket { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}