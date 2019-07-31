namespace Shop.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Brands", "CategoryId");
            CreateIndex("dbo.Brands", "CompanId");
            AddForeignKey("dbo.Brands", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Brands", "CompanId", "dbo.Companies", "CompanId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brands", "CompanId", "dbo.Companies");
            DropForeignKey("dbo.Brands", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Brands", new[] { "CompanId" });
            DropIndex("dbo.Brands", new[] { "CategoryId" });
        }
    }
}
