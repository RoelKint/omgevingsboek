namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class routeboek : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "BoekId", "dbo.Boeken");
            DropIndex("dbo.Routes", new[] { "BoekId" });
            CreateTable(
                "dbo.BoekRoutes",
                c => new
                    {
                        Boek_Id = c.Int(nullable: false),
                        Route_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boek_Id, t.Route_Id })
                .ForeignKey("dbo.Boeken", t => t.Boek_Id, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .Index(t => t.Boek_Id)
                .Index(t => t.Route_Id);
            
            DropColumn("dbo.Routes", "BoekId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "BoekId", c => c.Int(nullable: false));
            DropForeignKey("dbo.BoekRoutes", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.BoekRoutes", "Boek_Id", "dbo.Boeken");
            DropIndex("dbo.BoekRoutes", new[] { "Route_Id" });
            DropIndex("dbo.BoekRoutes", new[] { "Boek_Id" });
            DropTable("dbo.BoekRoutes");
            CreateIndex("dbo.Routes", "BoekId");
            AddForeignKey("dbo.Routes", "BoekId", "dbo.Boeken", "Id", cascadeDelete: true);
        }
    }
}
