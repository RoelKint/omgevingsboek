namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activiteits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EigenaarId = c.String(maxLength: 128),
                        Naam = c.String(nullable: false),
                        PoiId = c.Int(nullable: false),
                        MinLeeftijd = c.Int(nullable: false),
                        MaxLeeftijd = c.Int(nullable: false),
                        MinDuur = c.Int(nullable: false),
                        MaxDuur = c.Int(nullable: false),
                        Prijs = c.Double(nullable: false),
                        AfbeeldingNaam = c.String(),
                        DitactischeToelichting = c.String(),
                        Uitleg = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EigenaarId)
                .ForeignKey("dbo.Pois", t => t.PoiId, cascadeDelete: true)
                .Index(t => t.EigenaarId)
                .Index(t => t.PoiId);
            
            CreateTable(
                "dbo.Benodigdheids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Boeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        EigenaarId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EigenaarId)
                .Index(t => t.EigenaarId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        EigenaarID = c.Int(nullable: false),
                        Eigenaar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Eigenaar_Id)
                .Index(t => t.Eigenaar_Id);
            
            CreateTable(
                "dbo.Fotoboeks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pois",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        EigenaarId = c.String(maxLength: 128),
                        GeoLocatie = c.String(),
                        Email = c.String(),
                        Telefoon = c.Int(nullable: false),
                        Straat = c.String(),
                        Nummer = c.String(),
                        Gemeente = c.String(),
                        Postcode = c.Int(nullable: false),
                        MinLeeftijd = c.Int(nullable: false),
                        MaxLeeftijd = c.Int(nullable: false),
                        Prijs = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.EigenaarId)
                .Index(t => t.EigenaarId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BenodigdheidActiviteits",
                c => new
                    {
                        Benodigdheid_Id = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Benodigdheid_Id, t.Activiteit_Id })
                .ForeignKey("dbo.Benodigdheids", t => t.Benodigdheid_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .Index(t => t.Benodigdheid_Id)
                .Index(t => t.Activiteit_Id);
            
            CreateTable(
                "dbo.BoekActiviteits",
                c => new
                    {
                        Boek_Id = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Boek_Id, t.Activiteit_Id })
                .ForeignKey("dbo.Boeks", t => t.Boek_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .Index(t => t.Boek_Id)
                .Index(t => t.Activiteit_Id);
            
            CreateTable(
                "dbo.RouteActiviteits",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.Activiteit_Id })
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .Index(t => t.Route_Id)
                .Index(t => t.Activiteit_Id);
            
            CreateTable(
                "dbo.AspNetUserRoute",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        AspNetUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.AspNetUser_Id })
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id, cascadeDelete: true)
                .Index(t => t.Route_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUserBoek",
                c => new
                    {
                        Boek_Id = c.Int(nullable: false),
                        AspNetUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Boek_Id, t.AspNetUser_Id })
                .ForeignKey("dbo.Boeks", t => t.Boek_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id, cascadeDelete: true)
                .Index(t => t.Boek_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUserActiviteit",
                c => new
                    {
                        Activiteit_Id = c.Int(nullable: false),
                        AspNetUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Activiteit_Id, t.AspNetUser_Id })
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id, cascadeDelete: true)
                .Index(t => t.Activiteit_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.FotoboekActiviteits",
                c => new
                    {
                        Fotoboek_ID = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fotoboek_ID, t.Activiteit_Id })
                .ForeignKey("dbo.Fotoboeks", t => t.Fotoboek_ID, cascadeDelete: true)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .Index(t => t.Fotoboek_ID)
                .Index(t => t.Activiteit_Id);
            
            CreateTable(
                "dbo.TagActiviteits",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Activiteit_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Activiteit_Id);
            
            CreateTable(
                "dbo.VideoActiviteits",
                c => new
                    {
                        Video_ID = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Video_ID, t.Activiteit_Id })
                .ForeignKey("dbo.Videos", t => t.Video_ID, cascadeDelete: true)
                .ForeignKey("dbo.Activiteits", t => t.Activiteit_Id, cascadeDelete: true)
                .Index(t => t.Video_ID)
                .Index(t => t.Activiteit_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.VideoActiviteits", "Video_ID", "dbo.Videos");
            DropForeignKey("dbo.TagActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.TagActiviteits", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.Activiteits", "PoiId", "dbo.Pois");
            DropForeignKey("dbo.Pois", "EigenaarId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FotoboekActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.FotoboekActiviteits", "Fotoboek_ID", "dbo.Fotoboeks");
            DropForeignKey("dbo.Activiteits", "EigenaarId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserActiviteit", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserActiviteit", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.Boeks", "EigenaarId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserBoek", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserBoek", "Boek_Id", "dbo.Boeks");
            DropForeignKey("dbo.Routes", "Eigenaar_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoute", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoute", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.RouteActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.RouteActiviteits", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.BoekActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.BoekActiviteits", "Boek_Id", "dbo.Boeks");
            DropForeignKey("dbo.BenodigdheidActiviteits", "Activiteit_Id", "dbo.Activiteits");
            DropForeignKey("dbo.BenodigdheidActiviteits", "Benodigdheid_Id", "dbo.Benodigdheids");
            DropIndex("dbo.VideoActiviteits", new[] { "Activiteit_Id" });
            DropIndex("dbo.VideoActiviteits", new[] { "Video_ID" });
            DropIndex("dbo.TagActiviteits", new[] { "Activiteit_Id" });
            DropIndex("dbo.TagActiviteits", new[] { "Tag_ID" });
            DropIndex("dbo.FotoboekActiviteits", new[] { "Activiteit_Id" });
            DropIndex("dbo.FotoboekActiviteits", new[] { "Fotoboek_ID" });
            DropIndex("dbo.AspNetUserActiviteit", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserActiviteit", new[] { "Activiteit_Id" });
            DropIndex("dbo.AspNetUserBoek", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserBoek", new[] { "Boek_Id" });
            DropIndex("dbo.AspNetUserRoute", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserRoute", new[] { "Route_Id" });
            DropIndex("dbo.RouteActiviteits", new[] { "Activiteit_Id" });
            DropIndex("dbo.RouteActiviteits", new[] { "Route_Id" });
            DropIndex("dbo.BoekActiviteits", new[] { "Activiteit_Id" });
            DropIndex("dbo.BoekActiviteits", new[] { "Boek_Id" });
            DropIndex("dbo.BenodigdheidActiviteits", new[] { "Activiteit_Id" });
            DropIndex("dbo.BenodigdheidActiviteits", new[] { "Benodigdheid_Id" });
            DropIndex("dbo.Pois", new[] { "EigenaarId" });
            DropIndex("dbo.Routes", new[] { "Eigenaar_Id" });
            DropIndex("dbo.Boeks", new[] { "EigenaarId" });
            DropIndex("dbo.Activiteits", new[] { "PoiId" });
            DropIndex("dbo.Activiteits", new[] { "EigenaarId" });
            DropTable("dbo.VideoActiviteits");
            DropTable("dbo.TagActiviteits");
            DropTable("dbo.FotoboekActiviteits");
            DropTable("dbo.AspNetUserActiviteit");
            DropTable("dbo.AspNetUserBoek");
            DropTable("dbo.AspNetUserRoute");
            DropTable("dbo.RouteActiviteits");
            DropTable("dbo.BoekActiviteits");
            DropTable("dbo.BenodigdheidActiviteits");
            DropTable("dbo.Videos");
            DropTable("dbo.Tags");
            DropTable("dbo.Pois");
            DropTable("dbo.Fotoboeks");
            DropTable("dbo.Routes");
            DropTable("dbo.Boeks");
            DropTable("dbo.Benodigdheids");
            DropTable("dbo.Activiteits");
        }
    }
}
