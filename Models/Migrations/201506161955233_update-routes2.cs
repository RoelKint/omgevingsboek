namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateroutes2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Routes", new[] { "Eigenaar_Id" });
            DropColumn("dbo.Routes", "EigenaarID");
            RenameColumn(table: "dbo.Routes", name: "Eigenaar_Id", newName: "EigenaarID");
            AlterColumn("dbo.Routes", "EigenaarID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Routes", "EigenaarID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Routes", new[] { "EigenaarID" });
            AlterColumn("dbo.Routes", "EigenaarID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Routes", name: "EigenaarID", newName: "Eigenaar_Id");
            AddColumn("dbo.Routes", "EigenaarID", c => c.Int(nullable: false));
            CreateIndex("dbo.Routes", "Eigenaar_Id");
        }
    }
}
