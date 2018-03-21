namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGuessToPlayerGuessResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerGuessResults", "Guess", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayerGuessResults", "Guess");
        }
    }
}
