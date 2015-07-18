namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractorandCompanyCompleteFieldsAdded : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.People", newName: "Members");
            RenameTable(name: "dbo.CandidateVisits", newName: "ContractorVisits");
            DropForeignKey("dbo.SkillCandidates", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.SkillCandidates", "Candidate_Id", "dbo.People");
            DropIndex("dbo.SkillCandidates", new[] { "Skill_Id" });
            DropIndex("dbo.SkillCandidates", new[] { "Candidate_Id" });
            RenameColumn(table: "dbo.Blocks", name: "Person_Id", newName: "Member_Id");
            RenameColumn(table: "dbo.Schedules", name: "CandidateId", newName: "ContractorId");
            RenameColumn(table: "dbo.JobApplications", name: "CandidateId", newName: "ContractorId");
            RenameColumn(table: "dbo.ContractorVisits", name: "CandidateId", newName: "ContractorId");
            RenameColumn(table: "dbo.Blocks", name: "CandidateId", newName: "ContractorId");
            RenameIndex(table: "dbo.Blocks", name: "IX_CandidateId", newName: "IX_ContractorId");
            RenameIndex(table: "dbo.Blocks", name: "IX_Person_Id", newName: "IX_Member_Id");
            RenameIndex(table: "dbo.JobApplications", name: "IX_CandidateId", newName: "IX_ContractorId");
            RenameIndex(table: "dbo.Schedules", name: "IX_CandidateId", newName: "IX_ContractorId");
            RenameIndex(table: "dbo.ContractorVisits", name: "IX_CandidateId", newName: "IX_ContractorId");
            CreateTable(
                "dbo.FunctionalAreas",
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
            
            CreateTable(
                "dbo.ContractorSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractorId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        Proficiency = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        ExperienceInMonths = c.Int(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.ContractorId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        AlternareNumber = c.String(),
                        PictureUrl = c.String(),
                        LocationId = c.Int(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.CompanyId)
                .Index(t => t.LocationId);
            
            AddColumn("dbo.Members", "Profile", c => c.String());
            AddColumn("dbo.Members", "FirstName", c => c.String());
            AddColumn("dbo.Members", "LastName", c => c.String());
            AddColumn("dbo.Members", "AlternareNumber", c => c.String());
            AddColumn("dbo.Members", "OrganizationType", c => c.Int());
            AddColumn("dbo.Members", "RateType", c => c.Int());
            AddColumn("dbo.Members", "Gender", c => c.Int());
            AddColumn("dbo.Members", "ConsultantType", c => c.Int());
            AddColumn("dbo.Members", "ContractType", c => c.Int());
            AddColumn("dbo.Members", "FunctionalAreaId", c => c.Int());
            AddColumn("dbo.Members", "Nationality", c => c.String());
            AddColumn("dbo.Schedules", "Description", c => c.String());
            CreateIndex("dbo.Members", "FunctionalAreaId");
            AddForeignKey("dbo.Members", "FunctionalAreaId", "dbo.FunctionalAreas", "Id");
            DropColumn("dbo.Members", "Name");
            DropColumn("dbo.Schedules", "Company");
            DropColumn("dbo.Industries", "IndustryName");
            DropTable("dbo.SkillCandidates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SkillCandidates",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Candidate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Candidate_Id });
            
            AddColumn("dbo.Industries", "IndustryName", c => c.String());
            AddColumn("dbo.Schedules", "Company", c => c.String());
            AddColumn("dbo.Members", "Name", c => c.String());
            DropForeignKey("dbo.Contacts", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Contacts", "CompanyId", "dbo.Members");
            DropForeignKey("dbo.ContractorSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ContractorSkills", "ContractorId", "dbo.Members");
            DropForeignKey("dbo.Members", "FunctionalAreaId", "dbo.FunctionalAreas");
            DropIndex("dbo.Contacts", new[] { "LocationId" });
            DropIndex("dbo.Contacts", new[] { "CompanyId" });
            DropIndex("dbo.ContractorSkills", new[] { "SkillId" });
            DropIndex("dbo.ContractorSkills", new[] { "ContractorId" });
            DropIndex("dbo.Members", new[] { "FunctionalAreaId" });
            DropColumn("dbo.Schedules", "Description");
            DropColumn("dbo.Members", "Nationality");
            DropColumn("dbo.Members", "FunctionalAreaId");
            DropColumn("dbo.Members", "ContractType");
            DropColumn("dbo.Members", "ConsultantType");
            DropColumn("dbo.Members", "Gender");
            DropColumn("dbo.Members", "RateType");
            DropColumn("dbo.Members", "OrganizationType");
            DropColumn("dbo.Members", "AlternareNumber");
            DropColumn("dbo.Members", "LastName");
            DropColumn("dbo.Members", "FirstName");
            DropColumn("dbo.Members", "Profile");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContractorSkills");
            DropTable("dbo.FunctionalAreas");
            RenameIndex(table: "dbo.ContractorVisits", name: "IX_ContractorId", newName: "IX_CandidateId");
            RenameIndex(table: "dbo.Schedules", name: "IX_ContractorId", newName: "IX_CandidateId");
            RenameIndex(table: "dbo.JobApplications", name: "IX_ContractorId", newName: "IX_CandidateId");
            RenameIndex(table: "dbo.Blocks", name: "IX_Member_Id", newName: "IX_Person_Id");
            RenameIndex(table: "dbo.Blocks", name: "IX_ContractorId", newName: "IX_CandidateId");
            RenameColumn(table: "dbo.Blocks", name: "ContractorId", newName: "CandidateId");
            RenameColumn(table: "dbo.ContractorVisits", name: "ContractorId", newName: "CandidateId");
            RenameColumn(table: "dbo.JobApplications", name: "ContractorId", newName: "CandidateId");
            RenameColumn(table: "dbo.Schedules", name: "ContractorId", newName: "CandidateId");
            RenameColumn(table: "dbo.Blocks", name: "Member_Id", newName: "Person_Id");
            CreateIndex("dbo.SkillCandidates", "Candidate_Id");
            CreateIndex("dbo.SkillCandidates", "Skill_Id");
            AddForeignKey("dbo.SkillCandidates", "Candidate_Id", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SkillCandidates", "Skill_Id", "dbo.Skills", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.ContractorVisits", newName: "CandidateVisits");
            RenameTable(name: "dbo.Members", newName: "People");
        }
    }
}
