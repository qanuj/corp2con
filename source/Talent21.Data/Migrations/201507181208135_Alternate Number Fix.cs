namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlternateNumberFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Members", "AlternareNumber", "AlternateNumber");
            RenameColumn("dbo.Contacts", "AlternareNumber", "AlternateNumber");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Members", "AlternateNumber", "AlternareNumber");
            RenameColumn("dbo.Contacts", "AlternateNumber", "AlternareNumber");
        }
    }
}
