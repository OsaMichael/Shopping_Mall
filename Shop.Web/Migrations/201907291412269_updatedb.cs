namespace Shop.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Brands", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Brands", new[] { "CategoryId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Brands", "CategoryId");
            AddForeignKey("dbo.Brands", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
