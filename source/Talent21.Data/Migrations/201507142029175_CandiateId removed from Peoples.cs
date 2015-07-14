namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandiateIdremovedfromPeoples : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "CandidateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "CandidateId", c => c.Int());
        }
    }
}
