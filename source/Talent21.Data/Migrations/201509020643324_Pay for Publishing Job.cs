namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayforPublishingJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        Reason = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        Credit = c.Int(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
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
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.JobId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AdvertisementTransactions", "Name", c => c.String());
            AddColumn("dbo.Jobs", "Expiry", c => c.DateTime());
            Sql("Update dbo.Jobs set Expiry=DateAdd(month,1,Published) where IsPublished=1");
            AddColumn("dbo.Payments", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobTransactions", "JobId", "dbo.Jobs");
            DropIndex("dbo.JobTransactions", new[] { "UserId" });
            DropIndex("dbo.JobTransactions", new[] { "JobId" });
            DropColumn("dbo.Payments", "Name");
            DropColumn("dbo.Jobs", "Expiry");
            DropColumn("dbo.AdvertisementTransactions", "Name");
            DropTable("dbo.JobTransactions");
        }
    }
}
