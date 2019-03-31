namespace Ecom.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 222));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 222));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
