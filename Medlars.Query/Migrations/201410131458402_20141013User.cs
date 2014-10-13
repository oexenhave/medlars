namespace Medlars.Query.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20141013User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 150),
                        Secret = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
