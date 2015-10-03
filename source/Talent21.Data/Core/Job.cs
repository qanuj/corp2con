using System;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public interface IPubishable
    {
        bool IsPublished { get; set; }
        DateTime? Published { get; set; }
        DateTime? Expiry { get; set; }
    }

    public class Job : Dictionary, IPubishable
    {
        public string Description { get; set; }

        public IList<Location> Locations { get; set; }
        public IList<JobSkill> Skills { get; set; }
        public IList<JobVisit> Visits { get; set; }
        public IList<JobApplication> Applications { get; set; }
        public IList<JobTransaction> Transactions { get; set; }
        public IList<JobAdvertisement> Advertisements { get; set; }

        public int Rate { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public DateRangeData Duration { get; set; }
        public IntRangeData Experience { get; set; }

        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }

        public bool IsWorkingFromHome { get; set; }
        public bool IsContractExtendable { get; set; }
        public bool IsContractToHire { get; set; }

        public int Positions { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? Published { get; set; }
        public DateTime? Expiry { get; set; }


        public Job()
        {
            this.Skills = new List<JobSkill>();
            this.Advertisements = new List<JobAdvertisement>();
            this.Visits = new List<JobVisit>();
            this.Locations = new List<Location>();
            this.Applications = new List<JobApplication>();
            this.Transactions = new List<JobTransaction>();
            this.Duration=new DateRangeData();
            this.Experience=new IntRangeData();
        }
    }
}