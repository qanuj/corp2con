namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Companyscanpromotethemself : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LocationJobs", newName: "JobsLocationMapping");
            RenameColumn(table: "dbo.JobsLocationMapping", name: "Location_Id", newName: "LocationId");
            RenameColumn(table: "dbo.JobsLocationMapping", name: "Job_Id", newName: "JobId");
            RenameIndex(table: "dbo.JobsLocationMapping", name: "IX_Job_Id", newName: "IX_JobId");
            RenameIndex(table: "dbo.JobsLocationMapping", name: "IX_Location_Id", newName: "IX_LocationId");
            DropPrimaryKey("dbo.JobsLocationMapping");
            AddColumn("dbo.Advertisements", "CompanyId", c => c.Int());
            AddPrimaryKey("dbo.JobsLocationMapping", new[] { "JobId", "LocationId" });
            CreateIndex("dbo.Advertisements", "CompanyId");
            AddForeignKey("dbo.Advertisements", "CompanyId", "dbo.Members", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "CompanyId", "dbo.Members");
            DropIndex("dbo.Advertisements", new[] { "CompanyId" });
            DropPrimaryKey("dbo.JobsLocationMapping");
            DropColumn("dbo.Advertisements", "CompanyId");
            AddPrimaryKey("dbo.JobsLocationMapping", new[] { "Location_Id", "Job_Id" });
            RenameIndex(table: "dbo.JobsLocationMapping", name: "IX_LocationId", newName: "IX_Location_Id");
            RenameIndex(table: "dbo.JobsLocationMapping", name: "IX_JobId", newName: "IX_Job_Id");
            RenameColumn(table: "dbo.JobsLocationMapping", name: "JobId", newName: "Job_Id");
            RenameColumn(table: "dbo.JobsLocationMapping", name: "LocationId", newName: "Location_Id");
            RenameTable(name: "dbo.JobsLocationMapping", newName: "LocationJobs");
        }
    }
}
