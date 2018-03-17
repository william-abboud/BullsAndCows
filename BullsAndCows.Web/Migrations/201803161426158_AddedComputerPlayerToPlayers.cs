namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedComputerPlayerToPlayers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Players(PlayerId, Name, Score) VALUES(0, 'Computer', 0)");
        }
        
        public override void Down()
        {
        }
    }
}
