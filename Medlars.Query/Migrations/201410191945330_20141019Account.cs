namespace Medlars.Query.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20141019Account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "LastLogin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "LastLogin");
        }
    }
}
