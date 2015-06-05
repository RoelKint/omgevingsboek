namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupdatesoftdeletes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activiteits", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Boeks", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Routes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pois", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tags", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "IsDeleted");
            DropColumn("dbo.Pois", "IsDeleted");
            DropColumn("dbo.Routes", "IsDeleted");
            DropColumn("dbo.Boeks", "IsDeleted");
            DropColumn("dbo.Activiteits", "IsDeleted");
        }
    }
}
