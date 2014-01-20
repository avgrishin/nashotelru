namespace Nashotelru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResponse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Mail = c.String(nullable: false, maxLength: 200),
                        Text = c.String(nullable: false),
                        Text2 = c.String(),
                        IP = c.String(maxLength: 15),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Responses");
        }
    }
}
