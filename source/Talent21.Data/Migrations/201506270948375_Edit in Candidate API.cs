namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditinCandidateAPI : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Candidate_Id", "dbo.People");
            DropIndex("dbo.Skills", new[] { "Candidate_Id" });
            DropColumn("dbo.Skills", "CandidateId");
            RenameColumn(table: "dbo.Skills", name: "Candidate_Id", newName: "CandidateId");
            AlterColumn("dbo.Skills", "CandidateId", c => c.Int());
            CreateIndex("dbo.Skills", "CandidateId");
            AddForeignKey("dbo.Skills", "CandidateId", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.Industries", "IndustryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Industries", "IndustryName", c => c.String());
            DropForeignKey("dbo.Skills", "CandidateId", "dbo.People");
            DropIndex("dbo.Skills", new[] { "CandidateId" });
            AlterColumn("dbo.Skills", "CandidateId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Skills", name: "CandidateId", newName: "Candidate_Id");
            AddColumn("dbo.Skills", "CandidateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "Candidate_Id");
            AddForeignKey("dbo.Skills", "Candidate_Id", "dbo.People", "Id");
        }
    }
}
