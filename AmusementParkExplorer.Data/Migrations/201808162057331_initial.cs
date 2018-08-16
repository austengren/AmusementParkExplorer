namespace AmusementParkExplorer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Attraction", "ParkName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attraction", "ParkName", c => c.String(nullable: false));
        }
    }
}
