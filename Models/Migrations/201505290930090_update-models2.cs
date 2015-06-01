namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagPois",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Poi_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Poi_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Pois", t => t.Poi_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Poi_ID);
            
            AlterColumn("dbo.Pois", "Telefoon", c => c.String());
            AlterColumn("dbo.Tags", "Naam", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPois", "Poi_ID", "dbo.Pois");
            DropForeignKey("dbo.TagPois", "Tag_ID", "dbo.Tags");
            DropIndex("dbo.TagPois", new[] { "Poi_ID" });
            DropIndex("dbo.TagPois", new[] { "Tag_ID" });
            AlterColumn("dbo.Tags", "Naam", c => c.Int(nullable: false));
            AlterColumn("dbo.Pois", "Telefoon", c => c.Int(nullable: false));
            DropTable("dbo.TagPois");
        }
    }
}
