namespace Medlars.Query.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20141023Entry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        EntryId = c.Guid(nullable: false),
                        Service = c.String(nullable: false, maxLength: 128),
                        Message = c.String(nullable: false),
                        Severity = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AccountId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.EntryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Entries");
        }
    }
}
