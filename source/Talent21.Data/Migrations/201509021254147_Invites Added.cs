namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvitesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        Completed = c.DateTime(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CompanyId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invites", "CompanyId", "dbo.Members");
            DropIndex("dbo.Invites", new[] { "CompanyId" });
            DropTable("dbo.Invites");
        }
    }
}
