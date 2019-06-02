namespace Ecom.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 222));
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 222));
        }
    }
}
