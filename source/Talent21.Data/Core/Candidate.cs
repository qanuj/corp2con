using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Candidate : Member
    {
        public ICollection<CandidateSkill> Skills { get; set; }
        public ICollection<CandidateVisit> Visits { get; set; }
        public IList<Schedule> Schedules { get; set; }

        public Duration Experience { get; set; } //in Years and Months
        
        public string ProfileUrl { get; set; } //cv Url

        public int Rate { get; set; } //10k per month.
        public RateEnum RateType { get; set; }

        public GenderEnum Gender { get; set; }
        public ConsultantTypeEnum ConsultantType { get; set; }
        public ContractTypeEnum ContractType { get; set; }

        public Industry Industry { get; set; }
        public int? IndustryId { get; set; }

        public FunctionalArea FunctionalArea { get; set; }
        public int? FunctionalAreaId { get; set; }

        public string Nationality { get; set; }

        public Candidate()
        {
            Experience = new Duration();
        }
    }
}