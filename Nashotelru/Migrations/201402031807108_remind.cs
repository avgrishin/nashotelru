namespace Nashotelru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoUserInfoes", "ReminderToken", c => c.String(maxLength: 50));
            AddColumn("dbo.NoUserInfoes", "ReminderDT", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoUserInfoes", "ReminderDT");
            DropColumn("dbo.NoUserInfoes", "ReminderToken");
        }
    }
}
