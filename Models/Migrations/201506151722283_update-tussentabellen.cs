namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetussentabellen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BenodigdheidActiviteits", newName: "BenodigdheidActiviteiten");
            RenameTable(name: "dbo.BoekActiviteits", newName: "BoekActiviteiten");
            RenameTable(name: "dbo.AspNetUserActiviteit", newName: "AspNetUserActiviteiten");
            RenameTable(name: "dbo.FotoboekActiviteits", newName: "FotoboekActiviteiten");
            RenameTable(name: "dbo.TagActiviteits", newName: "TagActiviteiten");
            RenameTable(name: "dbo.VideoActiviteits", newName: "VideoActiviteiten");
            RenameTable(name: "dbo.AspNetUserBoek", newName: "AspNetUserBoeken");
            RenameTable(name: "dbo.AspNetUserRoute", newName: "AspNetUserRoutes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AspNetUserRoutes", newName: "AspNetUserRoute");
            RenameTable(name: "dbo.AspNetUserBoeken", newName: "AspNetUserBoek");
            RenameTable(name: "dbo.VideoActiviteiten", newName: "VideoActiviteits");
            RenameTable(name: "dbo.TagActiviteiten", newName: "TagActiviteits");
            RenameTable(name: "dbo.FotoboekActiviteiten", newName: "FotoboekActiviteits");
            RenameTable(name: "dbo.AspNetUserActiviteiten", newName: "AspNetUserActiviteit");
            RenameTable(name: "dbo.BoekActiviteiten", newName: "BoekActiviteits");
            RenameTable(name: "dbo.BenodigdheidActiviteiten", newName: "BenodigdheidActiviteits");
        }
    }
}
