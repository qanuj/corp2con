namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobhaveMultipleLocations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "LocationId", "dbo.Locations");
            DropIndex("dbo.Jobs", new[] { "LocationId" });
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Promotion = c.Int(nullable: false),
                        Title = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        JobId = c.Int(),
                        ContractorId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AdvertisementTransactions", t => t.TransactionId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.ContractorId, cascadeDelete: true)
                .Index(t => t.TransactionId)
                .Index(t => t.JobId)
                .Index(t => t.ContractorId);
            
            CreateTable(
                "dbo.AdvertisementTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        Credit = c.Int(nullable: false),
                        Code = c.String(),
                        PaymentCapture = c.String(),
                        UserId = c.String(maxLength: 128),
                        Amount = c.Single(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LocationJobs",
                c => new
                    {
                        Location_Id = c.Int(nullable: false),
                        Job_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Location_Id, t.Job_Id })
                .ForeignKey("dbo.Locations", t => t.Location_Id, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_Id, cascadeDelete: true)
                .Index(t => t.Location_Id)
                .Index(t => t.Job_Id);
            
            AddColumn("dbo.Jobs", "IsWorkingFromHome", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "Positions", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "Reason", c => c.String());
            AddColumn("dbo.Payments", "IsSuccess", c => c.Boolean(nullable: false));
            AddColumn("dbo.Payments", "Credit", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "PaymentCapture", c => c.String());
            AddColumn("dbo.Payments", "UserId", c => c.String(maxLength: 128));

            Sql("Insert into dbo.LocationJobs (Location_Id,Job_Id) select distinct LocationId,Id from dbo.Jobs");

            AlterColumn("dbo.Members", "OwnerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Payments", "Amount", c => c.Single(nullable: false));
            CreateIndex("dbo.Members", "OwnerId");
            CreateIndex("dbo.Payments", "UserId");
            AddForeignKey("dbo.Payments", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Members", "OwnerId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Jobs", "LocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "LocationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AdvertisementTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Advertisements", "ContractorId", "dbo.Members");
            DropForeignKey("dbo.Members", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LocationJobs", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.LocationJobs", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Advertisements", "TransactionId", "dbo.AdvertisementTransactions");
            DropForeignKey("dbo.Advertisements", "JobId", "dbo.Jobs");
            DropIndex("dbo.LocationJobs", new[] { "Job_Id" });
            DropIndex("dbo.LocationJobs", new[] { "Location_Id" });
            DropIndex("dbo.Payments", new[] { "UserId" });
            DropIndex("dbo.AdvertisementTransactions", new[] { "UserId" });
            DropIndex("dbo.Advertisements", new[] { "ContractorId" });
            DropIndex("dbo.Advertisements", new[] { "JobId" });
            DropIndex("dbo.Advertisements", new[] { "TransactionId" });
            DropIndex("dbo.Members", new[] { "OwnerId" });
            AlterColumn("dbo.Payments", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Members", "OwnerId", c => c.String());
            DropColumn("dbo.Payments", "UserId");
            DropColumn("dbo.Payments", "PaymentCapture");
            DropColumn("dbo.Payments", "Credit");
            DropColumn("dbo.Payments", "IsSuccess");
            DropColumn("dbo.Payments", "Reason");
            DropColumn("dbo.Jobs", "Positions");
            DropColumn("dbo.Jobs", "IsWorkingFromHome");
            DropTable("dbo.LocationJobs");
            DropTable("dbo.AdvertisementTransactions");
            DropTable("dbo.Advertisements");
            CreateIndex("dbo.Jobs", "LocationId");
            AddForeignKey("dbo.Jobs", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
