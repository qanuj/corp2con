namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Revoked : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobApplications", "Revoked", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobApplications", "Revoked", c => c.DateTime(nullable: false));
        }
    }
}
