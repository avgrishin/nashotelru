namespace Nashotelru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false));
        }
    }
}
