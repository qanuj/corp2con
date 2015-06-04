using System;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public abstract class Person : Entity, IPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
        public string OwnerId { get; set; }
        public string PictureUrl { get; set; }

        public Social Social { get; set; }
        public Location Location { get; set; }
        public int? LocationId { get; set; }

        public IList<Subscription> Subscriptions { get; set; }
        public IList<Block> Blocked { get; set; }
        protected Person()
        {
            this.Social = new Social();
        }
    }

    public class Contract : Dictionary
    {
        public string Description { get; set; }

        public Job Job { get; set; }
        public int JobId { get; set; }

        public bool IsFilled { get; set; }
        public string Location { get; set; }
    }

    public class JobSkill : SkillExtended
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }

    public enum CompanyOrCandidateEnum
    {
        Company,
        Candidate
    }

    public class Block : Entity
    {
        public CompanyOrCandidateEnum BlockedBy { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }

    public class Job : Dictionary
    {
        public string Description { get; set; }

        public Location Location { get; set; }
        public int LocationId { get; set; }

        public IList<JobSkill> Skills { get; set; }
        public IList<JobVisit> Visits { get; set; }
        public IList<JobApplication> Applications { get; set; }

        public int Rate { get; set; } //in 10's thousand.

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    [Flags]
    public enum JobActionEnum
    {
        Application,
        Favorite,
        Reported
    }

    public class JobApplication : Entity
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public JobActionEnum Act { get; set; }
    }

    public class JobVisit : Visit
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }

    public class Company : Person
    {
        public Industry Industry { get; set; }
        public int? IndustryId { get; set; }

        public IList<ContractSchedule> Schedules { get; set; }
        public IList<CompanyVisit> Visits { get; set; }
        public IList<Job> Jobs { get; set; }
    }

    public class ContractSchedule : Schedule
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public bool? IsBench { get; set; }
    }

    public class CompanyVisit : Visit
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }

    public class CandidateVisit : Visit
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }

    public abstract class Visit : Entity
    {
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Browser { get; set; }
        public string Referer { get; set; }
        public string OperatingSystem { get; set; }
        public bool IsMobile { get; set; }
    }

    public class Plan : Dictionary
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
        public bool IsHidden { get; set; }
        public int MonthlyFee { get; set; }
        public int AnnualFee { get; set; }
    }

    public class Subscription : Entity
    {
        public Person Subscriber { get; set; }
        public int SubscriberId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Payment Payment { get; set; }
        public int? PaymentId { get; set; }
    }

    public class Transaction : Entity
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }

    public class Payment : Transaction
    {
        public string Gateway { get; set; }
        public string Capture { get; set; }
    }

    public class Candidate : Person
    {
        public ICollection<CandidateSkill> Skills { get; set; }
        public ICollection<CandidateVisit> Visits { get; set; }
        public IList<Schedule> Schedules { get; set; }

        public Duration Experience { get; set; } //in Years and Months
        public string ProfileUrl { get; set; }
        public int Rate { get; set; } //10k per month.

        public Candidate()
        {
            Experience = new Duration();
        }
    }

    public class Schedule : Entity
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public bool IsAvailable { get; set; }
    }

    public class SkillExtended : Skill
    {
        public Skill Skill { get; set; }
        public LevelEnum Level { get; set; }
    }

    public class CandidateSkill : SkillExtended
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }

    public class Skill : Dictionary
    {

    }

    public class Industry : Dictionary
    {
        
    }

    public class Location : Dictionary
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
    }

}
