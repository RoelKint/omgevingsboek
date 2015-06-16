namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activiteitfotoupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FotoboekActiviteiten", "Fotoboek_ID", "dbo.Fotoboeken");
            DropForeignKey("dbo.FotoboekActiviteiten", "Activiteit_Id", "dbo.Activiteiten");
            DropIndex("dbo.FotoboekActiviteiten", new[] { "Fotoboek_ID" });
            DropIndex("dbo.FotoboekActiviteiten", new[] { "Activiteit_Id" });
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FotoUrl = c.String(),
                        Activiteit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activiteiten", t => t.Activiteit_Id)
                .Index(t => t.Activiteit_Id);
            DropTable("dbo.FotoboekActiviteiten");            
            DropTable("dbo.Fotoboeken");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FotoboekActiviteiten",
                c => new
                    {
                        Fotoboek_ID = c.Int(nullable: false),
                        Activiteit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fotoboek_ID, t.Activiteit_Id });
            
            CreateTable(
                "dbo.Fotoboeken",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Fotoes", "Activiteit_Id", "dbo.Activiteiten");
            DropIndex("dbo.Fotoes", new[] { "Activiteit_Id" });
            DropTable("dbo.Fotoes");
            CreateIndex("dbo.FotoboekActiviteiten", "Activiteit_Id");
            CreateIndex("dbo.FotoboekActiviteiten", "Fotoboek_ID");
            AddForeignKey("dbo.FotoboekActiviteiten", "Activiteit_Id", "dbo.Activiteiten", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FotoboekActiviteiten", "Fotoboek_ID", "dbo.Fotoboeken", "ID", cascadeDelete: true);
        }
    }
}
