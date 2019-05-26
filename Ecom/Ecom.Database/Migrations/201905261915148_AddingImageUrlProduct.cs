namespace Ecom.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImageUrlProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImagrUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImagrUrl");
        }
    }
}
