namespace Ecom.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsFeatured");
        }
    }
}
