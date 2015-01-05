namespace KaraageCounter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Karaages",
                c => new
                    {
                        KaraageID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KaraageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Karaages");
        }
    }
}
