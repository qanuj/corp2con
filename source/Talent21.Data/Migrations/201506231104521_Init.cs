namespace Talent21.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlockedBy = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.People", t => t.CandidateId)
                .ForeignKey("dbo.People", t => t.CompanyId)
                .Index(t => t.CompanyId)
                .Index(t => t.CandidateId)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        About = c.String(),
                        OwnerId = c.String(),
                        PictureUrl = c.String(),
                        Social_Twitter = c.String(),
                        Social_Facebook = c.String(),
                        Social_Yahoo = c.String(),
                        Social_Google = c.String(),
                        Social_LinkedIn = c.String(),
                        Social_Rss = c.String(),
                        Social_WebSite = c.String(),
                        LocationId = c.Int(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Experience_Years = c.Int(),
                        Experience_Months = c.Int(),
                        ProfileUrl = c.String(),
                        Rate = c.Int(),
                        CandidateId = c.Int(),
                        IsCancelled = c.Boolean(),
                        Cancelled = c.DateTime(),
                        IsRejected = c.Boolean(),
                        IndustryId = c.Int(),
                        CompanyId = c.Int(),
                        CompanyName = c.String(),
                        IsApproved = c.Boolean(),
                        Cancelled1 = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Industries", t => t.IndustryId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.IndustryId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        State = c.String(),
                        PinCode = c.String(),
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
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        IsAvailable = c.Boolean(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CompanyId = c.Int(),
                        IsBench = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CandidateId)
                .ForeignKey("dbo.People", t => t.CompanyId)
                .Index(t => t.CandidateId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryName = c.String(),
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
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        LocationId = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        Cancelled = c.DateTime(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CompanyId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Act = c.Int(nullable: false),
                        IsRevoked = c.Boolean(nullable: false),
                        Revoked = c.DateTime(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CandidateId)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .Index(t => t.CandidateId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        Title = c.String(),
                        Code = c.String(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Level = c.Int(),
                        JobId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Skill_Id = c.Int(),
                        Candidate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .ForeignKey("dbo.Skills", t => t.Skill_Id)
                .ForeignKey("dbo.People", t => t.Candidate_Id)
                .Index(t => t.JobId)
                .Index(t => t.Skill_Id)
                .Index(t => t.Candidate_Id);
            
            CreateTable(
                "dbo.JobVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Browser = c.String(),
                        Referer = c.String(),
                        OperatingSystem = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubscriberId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        PaymentId = c.Int(),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.PaymentId)
                .ForeignKey("dbo.People", t => t.SubscriberId)
                .Index(t => t.SubscriberId)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gateway = c.String(),
                        Capture = c.String(),
                        Code = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                "dbo.CompanyVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Browser = c.String(),
                        Referer = c.String(),
                        OperatingSystem = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.CandidateVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Browser = c.String(),
                        Referer = c.String(),
                        OperatingSystem = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        Deleted = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.CandidateId)
                .Index(t => t.CandidateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blocks", "CompanyId", "dbo.People");
            DropForeignKey("dbo.Blocks", "CandidateId", "dbo.People");
            DropForeignKey("dbo.CandidateVisits", "CandidateId", "dbo.People");
            DropForeignKey("dbo.CompanyVisits", "CompanyId", "dbo.People");
            DropForeignKey("dbo.Subscriptions", "SubscriberId", "dbo.People");
            DropForeignKey("dbo.People", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Blocks", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Subscriptions", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Schedules", "CompanyId", "dbo.People");
            DropForeignKey("dbo.JobVisits", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Skills", "Candidate_Id", "dbo.People");
            DropForeignKey("dbo.Skills", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.Skills", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Jobs", "CompanyId", "dbo.People");
            DropForeignKey("dbo.JobApplications", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobApplications", "CandidateId", "dbo.People");
            DropForeignKey("dbo.People", "IndustryId", "dbo.Industries");
            DropForeignKey("dbo.Schedules", "CandidateId", "dbo.People");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.CandidateVisits", new[] { "CandidateId" });
            DropIndex("dbo.CompanyVisits", new[] { "CompanyId" });
            DropIndex("dbo.Subscriptions", new[] { "PaymentId" });
            DropIndex("dbo.Subscriptions", new[] { "SubscriberId" });
            DropIndex("dbo.JobVisits", new[] { "JobId" });
            DropIndex("dbo.Skills", new[] { "Candidate_Id" });
            DropIndex("dbo.Skills", new[] { "Skill_Id" });
            DropIndex("dbo.Skills", new[] { "JobId" });
            DropIndex("dbo.JobApplications", new[] { "JobId" });
            DropIndex("dbo.JobApplications", new[] { "CandidateId" });
            DropIndex("dbo.Jobs", new[] { "CompanyId" });
            DropIndex("dbo.Jobs", new[] { "LocationId" });
            DropIndex("dbo.Schedules", new[] { "CompanyId" });
            DropIndex("dbo.Schedules", new[] { "CandidateId" });
            DropIndex("dbo.People", new[] { "IndustryId" });
            DropIndex("dbo.People", new[] { "LocationId" });
            DropIndex("dbo.Blocks", new[] { "Person_Id" });
            DropIndex("dbo.Blocks", new[] { "CandidateId" });
            DropIndex("dbo.Blocks", new[] { "CompanyId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.CandidateVisits");
            DropTable("dbo.CompanyVisits");
            DropTable("dbo.Payments");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.JobVisits");
            DropTable("dbo.Skills");
            DropTable("dbo.JobApplications");
            DropTable("dbo.Jobs");
            DropTable("dbo.Industries");
            DropTable("dbo.Schedules");
            DropTable("dbo.Locations");
            DropTable("dbo.People");
            DropTable("dbo.Blocks");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
