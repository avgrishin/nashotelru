namespace Nashotelru.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Title", c => c.String());
        }
    }
}
