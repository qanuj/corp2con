using System;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
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

        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? Published { get; set; }
    }
}