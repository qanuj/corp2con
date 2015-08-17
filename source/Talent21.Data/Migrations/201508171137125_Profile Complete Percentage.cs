namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileCompletePercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Complete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Complete");
        }
    }
}
