namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRoundsInFavorOfGuessResults : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rounds", newName: "PlayerGuessResults");
            DropColumn("dbo.PlayerGuessResults", "RoundNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerGuessResults", "RoundNumber", c => c.Int(nullable: false));
            RenameTable(name: "dbo.PlayerGuessResults", newName: "Rounds");
        }
    }
}
