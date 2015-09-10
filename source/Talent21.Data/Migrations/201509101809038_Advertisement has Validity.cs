namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvertisementhasValidity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppSiteConfigs", "Contractor_Highlight_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Highlight_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Featured_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Featured_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Global_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Global_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Advertise_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Advertise_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Highlight_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Highlight_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Featured_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Featured_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Global_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Global_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Advertise_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Advertise_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Highlight_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Highlight_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Featured_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Featured_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Global_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Global_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Advertise_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Advertise_Validity", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "JobPrice_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "JobPrice_Validity", c => c.Int(nullable: false));
            DropColumn("dbo.AppSiteConfigs", "Contractor_Highlight");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Featured");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Global");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Advertise");
            DropColumn("dbo.AppSiteConfigs", "Company_Highlight");
            DropColumn("dbo.AppSiteConfigs", "Company_Featured");
            DropColumn("dbo.AppSiteConfigs", "Company_Global");
            DropColumn("dbo.AppSiteConfigs", "Company_Advertise");
            DropColumn("dbo.AppSiteConfigs", "Job_Highlight");
            DropColumn("dbo.AppSiteConfigs", "Job_Featured");
            DropColumn("dbo.AppSiteConfigs", "Job_Global");
            DropColumn("dbo.AppSiteConfigs", "Job_Advertise");
            DropColumn("dbo.AppSiteConfigs", "JobPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppSiteConfigs", "JobPrice", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Advertise", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Global", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Featured", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Job_Highlight", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Advertise", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Global", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Featured", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Company_Highlight", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Advertise", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Global", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Featured", c => c.Int(nullable: false));
            AddColumn("dbo.AppSiteConfigs", "Contractor_Highlight", c => c.Int(nullable: false));
            DropColumn("dbo.AppSiteConfigs", "JobPrice_Validity");
            DropColumn("dbo.AppSiteConfigs", "JobPrice_Rate");
            DropColumn("dbo.AppSiteConfigs", "Job_Advertise_Validity");
            DropColumn("dbo.AppSiteConfigs", "Job_Advertise_Rate");
            DropColumn("dbo.AppSiteConfigs", "Job_Global_Validity");
            DropColumn("dbo.AppSiteConfigs", "Job_Global_Rate");
            DropColumn("dbo.AppSiteConfigs", "Job_Featured_Validity");
            DropColumn("dbo.AppSiteConfigs", "Job_Featured_Rate");
            DropColumn("dbo.AppSiteConfigs", "Job_Highlight_Validity");
            DropColumn("dbo.AppSiteConfigs", "Job_Highlight_Rate");
            DropColumn("dbo.AppSiteConfigs", "Company_Advertise_Validity");
            DropColumn("dbo.AppSiteConfigs", "Company_Advertise_Rate");
            DropColumn("dbo.AppSiteConfigs", "Company_Global_Validity");
            DropColumn("dbo.AppSiteConfigs", "Company_Global_Rate");
            DropColumn("dbo.AppSiteConfigs", "Company_Featured_Validity");
            DropColumn("dbo.AppSiteConfigs", "Company_Featured_Rate");
            DropColumn("dbo.AppSiteConfigs", "Company_Highlight_Validity");
            DropColumn("dbo.AppSiteConfigs", "Company_Highlight_Rate");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Advertise_Validity");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Advertise_Rate");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Global_Validity");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Global_Rate");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Featured_Validity");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Featured_Rate");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Highlight_Validity");
            DropColumn("dbo.AppSiteConfigs", "Contractor_Highlight_Rate");
        }
    }
}
