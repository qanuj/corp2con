namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountriesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Code = c.String(),
                    Deleted = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedBy = c.String(),
                    Created = c.DateTime(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropTable("dbo.Countries");
        }
    }
}
