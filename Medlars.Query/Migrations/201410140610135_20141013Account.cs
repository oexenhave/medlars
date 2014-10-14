namespace Medlars.Query.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20141013Account : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false, maxLength: 128),
                        PasswordHash = c.String(nullable: false, maxLength: 256),
                        PasswordSalt = c.String(nullable: false, maxLength: 256),
                        AllowedIps = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
