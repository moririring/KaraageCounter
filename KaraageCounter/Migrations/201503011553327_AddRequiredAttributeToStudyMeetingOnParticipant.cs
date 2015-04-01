namespace KaraageCounter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAttributeToStudyMeetingOnParticipant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Karaages", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Karaages", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Karaages", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Karaages", "UserName");
        }
    }
}
