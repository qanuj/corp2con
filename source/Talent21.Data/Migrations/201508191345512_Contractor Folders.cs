namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractorFolders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractorFolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractorId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Folder = c.String(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.CompanyId)
                .ForeignKey("dbo.Members", t => t.ContractorId)
                .Index(t => t.ContractorId)
                .Index(t => t.CompanyId);
            
            AddColumn("dbo.Members", "CompanyId1", c => c.Int());
            CreateIndex("dbo.Members", "CompanyId1");
            AddForeignKey("dbo.Members", "CompanyId1", "dbo.Members", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractorFolders", "ContractorId", "dbo.Members");
            DropForeignKey("dbo.ContractorFolders", "CompanyId", "dbo.Members");
            DropForeignKey("dbo.Members", "CompanyId1", "dbo.Members");
            DropIndex("dbo.ContractorFolders", new[] { "CompanyId" });
            DropIndex("dbo.ContractorFolders", new[] { "ContractorId" });
            DropIndex("dbo.Members", new[] { "CompanyId1" });
            DropColumn("dbo.Members", "CompanyId1");
            DropTable("dbo.ContractorFolders");
        }
    }
}
