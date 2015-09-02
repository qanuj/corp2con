namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovePaymenttoShared : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "TransactionId", "dbo.AdvertisementTransactions");
            DropIndex("dbo.Advertisements", new[] { "TransactionId" });
            AddColumn("dbo.AdvertisementTransactions", "AdvertisementId", c => c.Int(nullable: false));
            Sql("Update dbo.AdvertisementTransactions set AdvertisementId=(Select Top 1 Id from dbo.Advertisements where TransactionId=dbo.AdvertisementTransactions.Id)");
            CreateIndex("dbo.AdvertisementTransactions", "AdvertisementId");
            AddForeignKey("dbo.AdvertisementTransactions", "AdvertisementId", "dbo.Advertisements", "Id", cascadeDelete: true);
            DropColumn("dbo.Advertisements", "TransactionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "TransactionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AdvertisementTransactions", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.AdvertisementTransactions", new[] { "AdvertisementId" });
            DropColumn("dbo.AdvertisementTransactions", "AdvertisementId");
            CreateIndex("dbo.Advertisements", "TransactionId");
            AddForeignKey("dbo.Advertisements", "TransactionId", "dbo.AdvertisementTransactions", "Id", cascadeDelete: true);
        }
    }
}
