namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoverModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Movers", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movers", "Address", c => c.String());
            AlterColumn("dbo.Movers", "Name", c => c.String());
        }
    }
}
