namespace Nashotelru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailconfirm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoUserInfoes", "ConfirmationToken", c => c.String(maxLength: 50));
            AddColumn("dbo.NoUserInfoes", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoUserInfoes", "IsConfirmed");
            DropColumn("dbo.NoUserInfoes", "ConfirmationToken");
        }
    }
}
