namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexFieldsModifieds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Experience", c => c.Int());
            AddColumn("dbo.Jobs", "Duration_Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "Duration_End", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "Experience_Start", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Experience_End", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "IsContractExtendable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "IsContractToHire", c => c.Boolean(nullable: false));

            Sql("Update dbo.Members set Experience=(Experience_Years*12)+Experience_Months");

            DropColumn("dbo.Members", "Experience_Years");
            DropColumn("dbo.Members", "Experience_Months");
            DropColumn("dbo.Jobs", "Start");
            DropColumn("dbo.Jobs", "End");

            RenameColumn("dbo.ContractorSkills", "ExperienceInMonths", "Experience");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractorSkills", "ExperienceInMonths", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Members", "Experience_Months", c => c.Int());
            AddColumn("dbo.Members", "Experience_Years", c => c.Int());
            DropColumn("dbo.ContractorSkills", "Experience");
            DropColumn("dbo.Jobs", "IsContractToHire");
            DropColumn("dbo.Jobs", "IsContractExtendable");
            DropColumn("dbo.Jobs", "Experience_End");
            DropColumn("dbo.Jobs", "Experience_Start");
            DropColumn("dbo.Jobs", "Duration_End");
            DropColumn("dbo.Jobs", "Duration_Start");
            DropColumn("dbo.Members", "Experience");
        }
    }
}
