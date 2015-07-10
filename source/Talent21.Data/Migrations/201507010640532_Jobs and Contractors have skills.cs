namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobsandContractorshaveskills : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.Skills", "CandidateId", "dbo.People");
            DropForeignKey("dbo.Skills", "JobId", "dbo.Jobs");
            DropIndex("dbo.Skills", new[] { "Skill_Id" });
            RenameColumn(table: "dbo.Skills", name: "CandidateId", newName: "Candidate_Id");
            RenameColumn(table: "dbo.Skills", name: "JobId", newName: "Job_Id");
            RenameIndex(table: "dbo.Skills", name: "IX_JobId", newName: "IX_Job_Id");
            RenameIndex(table: "dbo.Skills", name: "IX_CandidateId", newName: "IX_Candidate_Id");
            AddColumn("dbo.Industries", "IndustryName", c => c.String());
            AddColumn("dbo.Jobs", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "Published", c => c.DateTime());
            AlterColumn("dbo.Jobs", "Cancelled", c => c.DateTime());
            AddForeignKey("dbo.Skills", "Candidate_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Skills", "Job_Id", "dbo.Jobs", "Id");
            DropColumn("dbo.Skills", "Level");
            DropColumn("dbo.Skills", "Discriminator");
            DropColumn("dbo.Skills", "Skill_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Skill_Id", c => c.Int());
            AddColumn("dbo.Skills", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Skills", "Level", c => c.Int());
            DropForeignKey("dbo.Skills", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Skills", "Candidate_Id", "dbo.People");
            AlterColumn("dbo.Jobs", "Cancelled", c => c.DateTime(nullable: false));
            DropColumn("dbo.Jobs", "Published");
            DropColumn("dbo.Jobs", "IsPublished");
            DropColumn("dbo.Industries", "IndustryName");
            RenameIndex(table: "dbo.Skills", name: "IX_Candidate_Id", newName: "IX_CandidateId");
            RenameIndex(table: "dbo.Skills", name: "IX_Job_Id", newName: "IX_JobId");
            RenameColumn(table: "dbo.Skills", name: "Job_Id", newName: "JobId");
            RenameColumn(table: "dbo.Skills", name: "Candidate_Id", newName: "CandidateId");
            CreateIndex("dbo.Skills", "Skill_Id");
            AddForeignKey("dbo.Skills", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "CandidateId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "Skill_Id", "dbo.Skills", "Id");
        }
    }
}
