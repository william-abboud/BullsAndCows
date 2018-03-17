namespace BullsAndCows.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedComputerPlayer : DbMigration
    {
        public override void Up()
        {
            var guid = new Guid().ToString();
            Sql($@"INSERT INTO AspNetUsers(Id, FullName, UserName, EmailConfirmed, PhoneNumberConfirmed, LockoutEnabled, TwoFactorEnabled, AccessFailedCount) VALUES({guid}, 'Computer', 'Computer', 1, 1, 0, 0, 0)");
        }
        
        public override void Down()
        {
        }
    }
}
