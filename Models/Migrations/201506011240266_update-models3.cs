namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boeks", "Afbeelding", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boeks", "Afbeelding");
        }
    }
}
