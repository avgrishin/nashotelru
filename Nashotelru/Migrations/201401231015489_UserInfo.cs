namespace Nashotelru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoUserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        EMail = c.String(maxLength: 200),
                        IsLocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "NoUserInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "NoUserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "NoUserInfo_Id", "dbo.NoUserInfoes", "Id");
            DropColumn("dbo.AspNetUsers", "EMail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "EMail", c => c.String(maxLength: 200));
            DropForeignKey("dbo.AspNetUsers", "NoUserInfo_Id", "dbo.NoUserInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "NoUserInfo_Id" });
            DropColumn("dbo.AspNetUsers", "NoUserInfo_Id");
            DropTable("dbo.NoUserInfoes");
        }
    }
}
