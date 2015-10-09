using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Contractor : Member
    {
        public ICollection<ContractorSkill> Skills { get; set; }
        public ICollection<ContractorVisit> Visits { get; set; }
        public ICollection<ContractorFolder> Folders { get; set; }
        public IList<Schedule> Schedules { get; set; }
        public IList<ContractorAdvertisement> Advertisements { get; set; }
        public IList<JobApplication> Applications { get; set; }

        public Company Company { get; set; }
        public int? CompanyId { get; set; }

        public int Experience { get; set; } //in Months
        
        public string ProfileUrl { get; set; } //cv Url

        public int Rate { get; set; } //10k per month.
        public RateEnum RateType { get; set; }

        public GenderEnum Gender { get; set; }
        public ContractorTypeEnum ConsultantType { get; set; }
        public ContractTypeEnum ContractType { get; set; }

        public FunctionalArea FunctionalArea { get; set; }
        public int? FunctionalAreaId { get; set; }

        public string Nationality { get; set; }

        [NotMapped]
        public int ExperienceMonths => Experience % 12;

        [NotMapped]
        // ReSharper disable once PossibleLossOfFraction
        public int ExperienceYears => (int) Math.Floor((decimal) (Experience / 12));

        public Contractor()
        {
            this.Skills = new List<ContractorSkill>();
            this.Visits = new List<ContractorVisit>();
            this.Folders = new List<ContractorFolder>();
            this.Schedules = new List<Schedule>();
            this.Advertisements=new List<ContractorAdvertisement>();
        }
    }
}