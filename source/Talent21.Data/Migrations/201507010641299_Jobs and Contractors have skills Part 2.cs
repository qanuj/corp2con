namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobsandContractorshaveskillsPart2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Skills", "Candidate_Id", "dbo.People");
            DropIndex("dbo.Skills", new[] { "Job_Id" });
            DropIndex("dbo.Skills", new[] { "Candidate_Id" });
            CreateTable(
                "dbo.SkillCandidates",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Candidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Candidate_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Candidate_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Candidate_Id);
            
            CreateTable(
                "dbo.SkillJobs",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Job_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Job_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Job_Id);
            
            DropColumn("dbo.Skills", "Job_Id");
            DropColumn("dbo.Skills", "Candidate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Candidate_Id", c => c.Int());
            AddColumn("dbo.Skills", "Job_Id", c => c.Int());
            DropForeignKey("dbo.SkillJobs", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.SkillJobs", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillCandidates", "Candidate_Id", "dbo.People");
            DropForeignKey("dbo.SkillCandidates", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillJobs", new[] { "Job_Id" });
            DropIndex("dbo.SkillJobs", new[] { "Skill_Id" });
            DropIndex("dbo.SkillCandidates", new[] { "Candidate_Id" });
            DropIndex("dbo.SkillCandidates", new[] { "Skill_Id" });
            DropTable("dbo.SkillJobs");
            DropTable("dbo.SkillCandidates");
            CreateIndex("dbo.Skills", "Candidate_Id");
            CreateIndex("dbo.Skills", "Job_Id");
            AddForeignKey("dbo.Skills", "Candidate_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Skills", "Job_Id", "dbo.Jobs", "Id");
        }
    }
}
