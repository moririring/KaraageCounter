namespace KaraageCounter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ranking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rankings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        IpAddress = c.String(),
                        SessionId = c.String(),
                        KaraageCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.KaraageRankings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KaraageRankings",
                c => new
                    {
                        KaraageRankingID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        IpAddress = c.String(),
                        SessionID = c.String(),
                        KaraageCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KaraageRankingID);
            
            DropTable("dbo.Rankings");
        }
    }
}
