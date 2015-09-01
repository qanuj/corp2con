namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisitshasVisitorName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobVisits", "Visitor", c => c.String());
            AddColumn("dbo.ContractorVisits", "Visitor", c => c.String());
            AddColumn("dbo.CompanyVisits", "Visitor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyVisits", "Visitor");
            DropColumn("dbo.ContractorVisits", "Visitor");
            DropColumn("dbo.JobVisits", "Visitor");
        }
    }
}
