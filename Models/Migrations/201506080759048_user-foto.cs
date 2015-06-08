namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userfoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Afbeelding", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Afbeelding");
        }
    }
}
