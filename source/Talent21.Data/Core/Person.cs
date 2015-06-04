using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public abstract class Person : Entity, IPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public string OwnerId { get; set; }
        public string PictureUrl { get; set; }

        public Social Social { get; set; }

        public IList<Subscription>  Subscriptions { get; set; }
        protected Person()
        {
            this.Social = new Social();
        }
    }

    public class Contract : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public bool IsFilled { get; set; }
        public string Location { get; set; }
    }

    public class Company : Person
    {
        public IList<ContractSchedule> Schedules { get; set; }
        public ICollection<CompanyVisit> Visits { get; set; }
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

    public class CandidateSkill : Entity
    {
        public Skill Skill { get; set; }
        public LevelEnum Level { get; set; }
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
        public int Rate { get; set; } //10k per month.
    }

    public class Skill : Dictionary
    {

    }

    public class Location : Dictionary
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
    }

}
