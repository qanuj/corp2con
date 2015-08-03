using System;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Job : Dictionary
    {
        public string Description { get; set; }

        public IList<Location> Locations { get; set; }
        public IList<JobSkill> Skills { get; set; }
        public IList<JobVisit> Visits { get; set; }
        public IList<JobApplication> Applications { get; set; }

        public int Rate { get; set; } //in 10's thousand.

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }

        public bool IsWorkingFromHome { get; set; }

        public int Positions { get; set; }

        public bool IsPublished
        { get; set; }
        public DateTime? Published { get; set; }

        public IList<JobAdvertisement> Advertisements { get; set; } 
    }

    public class JobAdvertisement : Advertisement
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }

    public class Advertisement : Entity
    {
        public PromotionEnum Promotion { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public AdvertisementTransaction Transaction { get; set; }
        public int TransactionId { get; set; }
    }

    public class ContractorAdvertisement : Advertisement
    {
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }
    }

    public enum PromotionEnum
    {
        None,
        Highlight,
        Feartured,
        Advertise
    }

    
}