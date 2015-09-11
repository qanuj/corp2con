namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackEmailSendandFeedbackisRead : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "IsRead", c => c.Boolean());
            AddColumn("dbo.AppSiteConfigs", "Notification_Feedback", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppSiteConfigs", "Notification_Feedback");
            DropColumn("dbo.Conversations", "IsRead");
        }
    }
}
