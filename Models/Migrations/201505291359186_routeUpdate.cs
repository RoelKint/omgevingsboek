namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class routeUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteActiviteits", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.RouteActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropIndex("dbo.RouteActiviteits", new[] { "Route_Id" });
            DropIndex("dbo.RouteActiviteits", new[] { "Activiteit_Id" });
            CreateTable(
                "dbo.RouteListItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RouteId = c.Int(nullable: false),
                        Adres = c.String(),
                        OrderIndex = c.Int(nullable: false),
                        Activiteit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId)
                .Index(t => t.Activiteit_Id);
            
            AddColumn("dbo.Routes", "Activiteit_Id", c => c.Int());
            CreateIndex("dbo.Routes", "Activiteit_Id");
            AddForeignKey("dbo.Routes", "Activiteit_Id", "dbo.Activiteits", "Id");
            DropTable("dbo.RouteActiviteits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RouteActiviteits",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.Activiteit_Id });
            
            DropForeignKey("dbo.Routes", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.RouteListItems", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.RouteListItems", "Activiteit_Id", "dbo.Activiteits");
            DropIndex("dbo.RouteListItems", new[] { "Activiteit_Id" });
            DropIndex("dbo.RouteListItems", new[] { "RouteId" });
            DropIndex("dbo.Routes", new[] { "Activiteit_Id" });
            DropColumn("dbo.Routes", "Activiteit_Id");
            DropTable("dbo.RouteListItems");
            CreateIndex("dbo.RouteActiviteits", "Activiteit_Id");
            CreateIndex("dbo.RouteActiviteits", "Route_Id");
            AddForeignKey("dbo.RouteActiviteits", "Activiteit_Id", "dbo.Activiteits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RouteActiviteits", "Route_Id", "dbo.Routes", "Id", cascadeDelete: true);
        }
    }
}
