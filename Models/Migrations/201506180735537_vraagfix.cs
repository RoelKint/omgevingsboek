namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vraagfix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Vraags", new[] { "Eigenaar_Id" });
            DropColumn("dbo.Vraags", "EigenaarId");
            RenameColumn(table: "dbo.Vraags", name: "Eigenaar_Id", newName: "EigenaarId");
            AlterColumn("dbo.Vraags", "EigenaarId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Vraags", "EigenaarId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Vraags", new[] { "EigenaarId" });
            AlterColumn("dbo.Vraags", "EigenaarId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Vraags", name: "EigenaarId", newName: "Eigenaar_Id");
            AddColumn("dbo.Vraags", "EigenaarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vraags", "Eigenaar_Id");
        }
    }
}
