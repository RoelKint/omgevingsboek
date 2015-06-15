namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateboeken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoekOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EigenaarId = c.String(maxLength: 128),
                        Index = c.Int(nullable: false),
                        BoekId = c.Int(nullable: false),
                        IsSharedLijst = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boeks", t => t.BoekId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EigenaarId)
                .Index(t => t.EigenaarId)
                .Index(t => t.BoekId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoekOrders", "EigenaarId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BoekOrders", "BoekId", "dbo.Boeks");
            DropIndex("dbo.BoekOrders", new[] { "BoekId" });
            DropIndex("dbo.BoekOrders", new[] { "EigenaarId" });
            DropTable("dbo.BoekOrders");
        }
    }
}
