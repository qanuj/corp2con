namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidatesareContractors : DbMigration
    {
        public override void Up()
        {
            Sql("Update Members set Discriminator='Contractor' where Discriminator='Candidate'");
        }
        
        public override void Down()
        {
        }
    }
}
