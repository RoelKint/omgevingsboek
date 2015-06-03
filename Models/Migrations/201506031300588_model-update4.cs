namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupdate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pois", "Afbeelding", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pois", "Afbeelding");
        }
    }
}
