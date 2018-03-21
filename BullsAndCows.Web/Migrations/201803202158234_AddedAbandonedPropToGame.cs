namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAbandonedPropToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "IsAbandoned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "IsAbandoned");
        }
    }
}
