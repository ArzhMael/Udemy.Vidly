namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "CreatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "DateAdded");
        }
    }
}
