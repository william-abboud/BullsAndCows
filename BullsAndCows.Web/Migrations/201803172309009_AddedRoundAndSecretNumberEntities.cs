namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRoundAndSecretNumberEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoundNumber = c.Int(nullable: false),
                        BullsGuessed = c.Int(nullable: false),
                        CowsGuessed = c.Int(nullable: false),
                        PlayerId = c.String(maxLength: 128),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .Index(t => t.PlayerId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.SecretNumbers",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlayerId = c.String(nullable: false, maxLength: 128),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlayerId })
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecretNumbers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.SecretNumbers", "GameId", "dbo.Games");
            DropForeignKey("dbo.Rounds", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Rounds", "GameId", "dbo.Games");
            DropIndex("dbo.SecretNumbers", new[] { "PlayerId" });
            DropIndex("dbo.SecretNumbers", new[] { "GameId" });
            DropIndex("dbo.Rounds", new[] { "GameId" });
            DropIndex("dbo.Rounds", new[] { "PlayerId" });
            DropTable("dbo.SecretNumbers");
            DropTable("dbo.Rounds");
        }
    }
}
