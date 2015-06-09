namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepoimodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pois", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Pois", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.Pois", "GeoLocatie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pois", "GeoLocatie", c => c.String());
            DropColumn("dbo.Pois", "Latitude");
            DropColumn("dbo.Pois", "Longitude");
        }
    }
}
