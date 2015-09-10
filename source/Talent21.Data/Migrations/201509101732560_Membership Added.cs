namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppSiteConfigs", "JobPrice", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "ContractorMembership_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "ContractorMembership_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "CompanyMembership_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "CompanyMembership_Validity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppSiteConfigs", "CompanyMembership_Validity");
            DropColumn("dbo.AppSiteConfigs", "CompanyMembership_Rate");
            DropColumn("dbo.AppSiteConfigs", "ContractorMembership_Validity");
            DropColumn("dbo.AppSiteConfigs", "ContractorMembership_Rate");
            DropColumn("dbo.AppSiteConfigs", "JobPrice");
        }
    }
}
