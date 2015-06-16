namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablenames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Activiteits", newName: "Activiteiten");
            RenameTable(name: "dbo.Benodigdheids", newName: "Benodigdheden");
            RenameTable(name: "dbo.Boeks", newName: "Boeken");
            RenameTable(name: "dbo.Fotoboeks", newName: "Fotoboeken");
            RenameTable(name: "dbo.BoekOrders", newName: "BoekOrder");
            RenameTable(name: "dbo.Uitnodigings", newName: "Uitnodigingen");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Uitnodigingen", newName: "Uitnodigings");
            RenameTable(name: "dbo.BoekOrder", newName: "BoekOrders");
            RenameTable(name: "dbo.Fotoboeken", newName: "Fotoboeks");
            RenameTable(name: "dbo.Boeken", newName: "Boeks");
            RenameTable(name: "dbo.Benodigdheden", newName: "Benodigdheids");
            RenameTable(name: "dbo.Activiteiten", newName: "Activiteits");
        }
    }
}
