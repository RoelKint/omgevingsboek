namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatehelpdesk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vraags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Omschrhijving = c.String(),
                        EigenaarId = c.Int(nullable: false),
                        IsGelezen = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        Eigenaar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Eigenaar_Id)
                .Index(t => t.Eigenaar_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vraags", "Eigenaar_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Vraags", new[] { "Eigenaar_Id" });
            DropTable("dbo.Vraags");
        }
    }
}
