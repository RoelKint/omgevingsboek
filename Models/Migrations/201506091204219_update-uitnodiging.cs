namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuitnodiging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uitnodigings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EigenaarId = c.String(maxLength: 128),
                        Key = c.String(),
                        Gebruikt = c.Boolean(nullable: false),
                        GebruiktDoorId = c.String(maxLength: 128),
                        EmailUitgenodigde = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EigenaarId)
                .ForeignKey("dbo.AspNetUsers", t => t.GebruiktDoorId)
                .Index(t => t.EigenaarId)
                .Index(t => t.GebruiktDoorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uitnodigings", "GebruiktDoorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Uitnodigings", "EigenaarId", "dbo.AspNetUsers");
            DropIndex("dbo.Uitnodigings", new[] { "GebruiktDoorId" });
            DropIndex("dbo.Uitnodigings", new[] { "EigenaarId" });
            DropTable("dbo.Uitnodigings");
        }
    }
}
