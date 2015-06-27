namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateFixes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "IsCancelled");
            DropColumn("dbo.People", "Cancelled");
            DropColumn("dbo.People", "IsRejected");
            DropColumn("dbo.People", "IsApproved");
            DropColumn("dbo.People", "Cancelled1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Cancelled1", c => c.DateTime());
            AddColumn("dbo.People", "IsApproved", c => c.Boolean());
            AddColumn("dbo.People", "IsRejected", c => c.Boolean());
            AddColumn("dbo.People", "Cancelled", c => c.DateTime());
            AddColumn("dbo.People", "IsCancelled", c => c.Boolean());
        }
    }
}
