namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoverPropAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movers", "Address", c => c.String());
            AddColumn("dbo.Movers", "PostedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movers", "PostedOn");
            DropColumn("dbo.Movers", "Address");
        }
    }
}
