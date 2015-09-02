namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionsIntoSingleTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AdvertisementTransactions", newName: "Transactions");
            DropForeignKey("dbo.Advertisements", "TransactionId", "dbo.AdvertisementTransactions");
            DropForeignKey("dbo.Subscriptions", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Advertisements", new[] { "TransactionId" });
            DropIndex("dbo.Payments", new[] { "UserId" });
            AddColumn("dbo.Transactions", "Name", c => c.String());
            AddColumn("dbo.Transactions", "AdvertisementId", c => c.Int());
            Sql("Update dbo.Transactions set AdvertisementId=(Select Top 1 Id from dbo.Advertisements where TransactionId=dbo.Transactions.Id)");
            AddColumn("dbo.Transactions", "JobId", c => c.Int());
            AddColumn("dbo.Transactions", "Gateway", c => c.String());
            AddColumn("dbo.Transactions", "Capture", c => c.String());
            AddColumn("dbo.Transactions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Jobs", "Expiry", c => c.DateTime());
            CreateIndex("dbo.Transactions", "AdvertisementId");
            CreateIndex("dbo.Transactions", "JobId");
            AddForeignKey("dbo.Transactions", "AdvertisementId", "dbo.Advertisements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "JobId", "dbo.Jobs", "Id");
            DropColumn("dbo.Advertisements", "TransactionId");
            DropTable("dbo.Payments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gateway = c.String(),
                        Capture = c.String(),
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Advertisements", "TransactionId", c => c.Int(nullable: false));
            Sql("Update dbo.Advertisements set TransactionId=(Select Top 1 Id from dbo.Transactions where AdvertisementId=dbo.Advertisements.Id)");
            DropForeignKey("dbo.Transactions", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Transactions", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.Transactions", new[] { "JobId" });
            DropIndex("dbo.Transactions", new[] { "AdvertisementId" });
            DropColumn("dbo.Jobs", "Expiry");
            DropColumn("dbo.Transactions", "Discriminator");
            DropColumn("dbo.Transactions", "Capture");
            DropColumn("dbo.Transactions", "Gateway");
            DropColumn("dbo.Transactions", "JobId");
            DropColumn("dbo.Transactions", "AdvertisementId");
            DropColumn("dbo.Transactions", "Name");
            CreateIndex("dbo.Payments", "UserId");
            CreateIndex("dbo.Advertisements", "TransactionId");
            AddForeignKey("dbo.Advertisements", "TransactionId", "dbo.AdvertisementTransactions", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Transactions", newName: "AdvertisementTransactions");
        }
    }
}
