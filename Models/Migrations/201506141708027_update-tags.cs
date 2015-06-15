namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagPois", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.TagPois", "Poi_ID", "dbo.Pois");
            DropIndex("dbo.TagPois", new[] { "Tag_ID" });
            DropIndex("dbo.TagPois", new[] { "Poi_ID" });
            CreateTable(
                "dbo.PoiTags",
                c => new
                    {
                        PoiId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        EigenaarId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PoiId, t.TagId })
                .ForeignKey("dbo.AspNetUsers", t => t.EigenaarId)
                .ForeignKey("dbo.Pois", t => t.PoiId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PoiId)
                .Index(t => t.TagId)
                .Index(t => t.EigenaarId);
            
            DropTable("dbo.TagPois");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagPois",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Poi_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Poi_ID });
            
            DropForeignKey("dbo.PoiTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PoiTags", "PoiId", "dbo.Pois");
            DropForeignKey("dbo.PoiTags", "EigenaarId", "dbo.AspNetUsers");
            DropIndex("dbo.PoiTags", new[] { "EigenaarId" });
            DropIndex("dbo.PoiTags", new[] { "TagId" });
            DropIndex("dbo.PoiTags", new[] { "PoiId" });
            DropTable("dbo.PoiTags");
            CreateIndex("dbo.TagPois", "Poi_ID");
            CreateIndex("dbo.TagPois", "Tag_ID");
            AddForeignKey("dbo.TagPois", "Poi_ID", "dbo.Pois", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TagPois", "Tag_ID", "dbo.Tags", "ID", cascadeDelete: true);
        }
    }
}
