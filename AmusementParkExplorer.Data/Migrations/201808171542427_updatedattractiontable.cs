namespace AmusementParkExplorer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedattractiontable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attraction", "ParkID", "dbo.Park");
            DropIndex("dbo.Attraction", new[] { "ParkID" });
            DropColumn("dbo.Attraction", "ParkName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attraction", "ParkName", c => c.String());
            CreateIndex("dbo.Attraction", "ParkID");
            AddForeignKey("dbo.Attraction", "ParkID", "dbo.Park", "ParkID", cascadeDelete: true);
        }
    }
}
