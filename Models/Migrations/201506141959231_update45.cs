namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update45 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PoiTags", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PoiTags", "Id", c => c.Int(nullable: false));
        }
    }
}
