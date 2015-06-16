namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateroutes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "BoekId", c => c.Int(nullable: false));
            CreateIndex("dbo.Routes", "BoekId");
            AddForeignKey("dbo.Routes", "BoekId", "dbo.Boeken", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "BoekId", "dbo.Boeken");
            DropIndex("dbo.Routes", new[] { "BoekId" });
            DropColumn("dbo.Routes", "BoekId");
        }
    }
}
