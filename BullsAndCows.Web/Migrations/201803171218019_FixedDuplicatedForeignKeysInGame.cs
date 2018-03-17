namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDuplicatedForeignKeysInGame : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Games", new[] { "PlayerOne_PlayerId" });
            DropIndex("dbo.Games", new[] { "PlayerTwo_PlayerId" });
            DropColumn("dbo.Games", "PlayerOneId");
            DropColumn("dbo.Games", "PlayerTwoId");
            RenameColumn(table: "dbo.Games", name: "PlayerOne_PlayerId", newName: "PlayerOneId");
            RenameColumn(table: "dbo.Games", name: "PlayerTwo_PlayerId", newName: "PlayerTwoId");
            AlterColumn("dbo.Games", "PlayerOneId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Games", "PlayerTwoId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Games", "PlayerOneId");
            CreateIndex("dbo.Games", "PlayerTwoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "PlayerTwoId" });
            DropIndex("dbo.Games", new[] { "PlayerOneId" });
            AlterColumn("dbo.Games", "PlayerTwoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "PlayerOneId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Games", name: "PlayerTwoId", newName: "PlayerTwo_PlayerId");
            RenameColumn(table: "dbo.Games", name: "PlayerOneId", newName: "PlayerOne_PlayerId");
            AddColumn("dbo.Games", "PlayerTwoId", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "PlayerOneId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "PlayerTwo_PlayerId");
            CreateIndex("dbo.Games", "PlayerOne_PlayerId");
        }
    }
}
