namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyaddedtoSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "Company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "Company");
        }
    }
}
