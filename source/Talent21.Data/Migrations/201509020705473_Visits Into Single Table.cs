namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisitsIntoSingleTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.JobVisits", newName: "Visits");
            DropIndex("dbo.Visits", new[] { "JobId" });
            DropIndex("dbo.ContractorVisits", new[] { "ContractorId" });
            DropIndex("dbo.CompanyVisits", new[] { "CompanyId" });
            AddColumn("dbo.Visits", "ContractorId", c => c.Int());
            AddColumn("dbo.Visits", "CompanyId", c => c.Int());
            AddColumn("dbo.Visits", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Visits", "JobId", c => c.Int());
            CreateIndex("dbo.Visits", "JobId");
            CreateIndex("dbo.Visits", "ContractorId");
            CreateIndex("dbo.Visits", "CompanyId");
            DropTable("dbo.ContractorVisits");
            DropTable("dbo.CompanyVisits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CompanyVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Browser = c.String(),
                        Referer = c.String(),
                        OperatingSystem = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        Visitor = c.String(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractorVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractorId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Browser = c.String(),
                        Referer = c.String(),
                        OperatingSystem = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        Visitor = c.String(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Visits", new[] { "CompanyId" });
            DropIndex("dbo.Visits", new[] { "ContractorId" });
            DropIndex("dbo.Visits", new[] { "JobId" });
            AlterColumn("dbo.Visits", "JobId", c => c.Int(nullable: false));
            DropColumn("dbo.Visits", "Discriminator");
            DropColumn("dbo.Visits", "CompanyId");
            DropColumn("dbo.Visits", "ContractorId");
            CreateIndex("dbo.CompanyVisits", "CompanyId");
            CreateIndex("dbo.ContractorVisits", "ContractorId");
            CreateIndex("dbo.Visits", "JobId");
            RenameTable(name: "dbo.Visits", newName: "JobVisits");
        }
    }
}
