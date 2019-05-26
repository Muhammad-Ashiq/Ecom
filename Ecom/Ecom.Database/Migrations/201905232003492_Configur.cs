namespace Ecom.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Configur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Configs");
        }
    }
}
