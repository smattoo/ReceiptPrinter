namespace ReceiptPrinter.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingBasket",
                c => new
                    {
                        ShoppingBasketRowId = c.Guid(nullable: false),
                        ShoppingBasketNumber = c.Int(nullable: false),
                        ProductDetail_Qty = c.Int(nullable: false),
                        ProductDetail_Price = c.Double(nullable: false),
                        ProductDetail_IsImported = c.Boolean(nullable: false),
                        ProductDetail_ProductType = c.Int(nullable: false),
                        ProductDetail_ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ShoppingBasketRowId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingBasket");
        }
    }
}
