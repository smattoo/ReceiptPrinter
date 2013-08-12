using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Domain;

namespace ReceiptPrinterTests
{
    public static class TestRepository
    {
        public static IEnumerable<ShoppingBasket> GetShoppingBaskets()
        {
            //  This method will be called after migrating to the latest version.
            var shoppingBaskets = new List<ShoppingBasket>
            {
                new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 1,
                    ProductDetail = new ProductDetail { ProductName = "book",
                                                        IsImported = false,
                                                        ProductType = ProductType.Books,
                                                        Price = 12.49,
                                                        Qty = 1
                    }
                },

                new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 1,
                    ProductDetail = new ProductDetail { ProductName  = "music CD",
                                                        IsImported = false,
                                                        ProductType = ProductType.Other,
                                                        Price = 14.99,
                                                        Qty = 1
                        }
                },

                 new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 1,
                    ProductDetail = new ProductDetail { ProductName = "chocolate bar",
                                                        IsImported = false,
                                                        ProductType = ProductType.Food,
                                                        Price = 0.85,
                                                        Qty = 1
                        }
                },

                 new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 2,
                    ProductDetail = new ProductDetail { ProductName = "box of chocolates",
                                                        IsImported = true,
                                                        ProductType = ProductType.Food,
                                                        Price = 10.00,
                                                        Qty = 1
                        }
                },

                 new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 2,
                    ProductDetail = new ProductDetail { ProductName = "bottle of perfume",
                                                        IsImported = true,
                                                        ProductType = ProductType.Other,
                                                        Price = 47.50,
                                                        Qty = 1
                        }
                },


                 new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 3,
                    ProductDetail = new ProductDetail { ProductName = "bottle of perfume",
                                                        IsImported = true,
                                                        ProductType = ProductType.Other,
                                                        Price = 27.99,
                                                        Qty = 1
                        }
                },


                 new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 3,
                    ProductDetail = new ProductDetail { ProductName = "bottle of perfume",
                                                        IsImported = false,
                                                        ProductType = ProductType.Other,
                                                        Price = 18.99,
                                                        Qty = 1
                        }
                },

                  new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 3,
                    ProductDetail = new ProductDetail { ProductName = "packet of headache pills",
                                                        IsImported = false,
                                                        ProductType = ProductType.Medical,
                                                        Price = 9.75,
                                                        Qty = 1
                        }
                },


                  new ShoppingBasket
                {
                    ShoppingBasketRowId = Guid.NewGuid(),
                    ShoppingBasketNumber = 3,
                    ProductDetail = new ProductDetail {  ProductName = "box of chocolates",
                                                        IsImported = true,
                                                        ProductType = ProductType.Food,
                                                        Price = 11.25,
                                                        Qty = 1
                        }
                }
            };

            return shoppingBaskets;
        }
    }
}
