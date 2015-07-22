namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobsHaveskills : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkillJobs", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillJobs", "Job_Id", "dbo.Jobs");
            DropIndex("dbo.SkillJobs", new[] { "Skill_Id" });
            DropIndex("dbo.SkillJobs", new[] { "Job_Id" });
            CreateTable(
                "dbo.JobSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.SkillId);
            
            DropTable("dbo.SkillJobs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SkillJobs",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Job_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Job_Id });
            
            DropForeignKey("dbo.JobSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.JobSkills", "JobId", "dbo.Jobs");
            DropIndex("dbo.JobSkills", new[] { "SkillId" });
            DropIndex("dbo.JobSkills", new[] { "JobId" });
            DropTable("dbo.JobSkills");
            CreateIndex("dbo.SkillJobs", "Job_Id");
            CreateIndex("dbo.SkillJobs", "Skill_Id");
            AddForeignKey("dbo.SkillJobs", "Job_Id", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SkillJobs", "Skill_Id", "dbo.Skills", "Id", cascadeDelete: true);
        }
    }
}
