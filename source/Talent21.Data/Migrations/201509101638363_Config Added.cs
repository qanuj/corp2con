namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppSiteConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contractor_Highlight = c.Int(nullable: false),
                        Contractor_Featured = c.Int(nullable: false),
                        Contractor_Global = c.Int(nullable: false),
                        Contractor_Advertise = c.Int(nullable: false),
                        Company_Highlight = c.Int(nullable: false),
                        Company_Featured = c.Int(nullable: false),
                        Company_Global = c.Int(nullable: false),
                        Company_Advertise = c.Int(nullable: false),
                        Job_Highlight = c.Int(nullable: false),
                        Job_Featured = c.Int(nullable: false),
                        Job_Global = c.Int(nullable: false),
                        Job_Advertise = c.Int(nullable: false),
                        Payment_Key = c.String(),
                        Payment_Salt = c.String(),
                        Payment_MerchantId = c.String(),
                        Payment_Url = c.String(),
                        Credit_Rate = c.Int(nullable: false),
                        Credit_Validity = c.Int(nullable: false),
                        Tax_Name = c.String(),
                        Tax_Rate = c.Double(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppSiteConfigs");
        }
    }
}
