namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobApplicationHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobApplicationHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.Int(nullable: false),
                        Act = c.Int(nullable: false),
                        Notes = c.String(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            AddColumn("dbo.JobApplications", "Folder", c => c.String());
            DropColumn("dbo.JobApplications", "Act");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobApplications", "Act", c => c.Int(nullable: false));
            DropForeignKey("dbo.JobApplicationHistories", "ApplicationId", "dbo.JobApplications");
            DropIndex("dbo.JobApplicationHistories", new[] { "ApplicationId" });
            DropColumn("dbo.JobApplications", "Folder");
            DropTable("dbo.JobApplicationHistories");
        }
    }
}
