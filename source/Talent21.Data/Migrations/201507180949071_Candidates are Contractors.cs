namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidatesareContractors : DbMigration
    {
        public override void Up()
        {
            Sql("Update Members set Discriminator='Contractor',ConsultantType=0,ContractType=0,Gender=0,RateType=0 where Discriminator='Candidate'");
            Sql("Update Members set OrganizationType=0,ContractType=0,Gender=0 where Discriminator='Company'");
        }
        
        public override void Down()
        {
        }
    }
}
