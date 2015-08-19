namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyIdfromCompanyRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "CompanyId");
            RenameColumn(table: "dbo.Members", name: "CompanyId1", newName: "CompanyId");
            RenameIndex(table: "dbo.Members", name: "IX_CompanyId1", newName: "IX_CompanyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Members", name: "IX_CompanyId", newName: "IX_CompanyId1");
            RenameColumn(table: "dbo.Members", name: "CompanyId", newName: "CompanyId1");
            AddColumn("dbo.Members", "CompanyId", c => c.Int());
        }
    }
}
