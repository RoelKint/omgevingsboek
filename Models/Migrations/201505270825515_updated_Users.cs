namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_Users : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Naam", c => c.String());
            AddColumn("dbo.AspNetUsers", "Voornaam", c => c.String());
            AddColumn("dbo.AspNetUsers", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Deleted");
            DropColumn("dbo.AspNetUsers", "Voornaam");
            DropColumn("dbo.AspNetUsers", "Naam");
        }
    }
}
