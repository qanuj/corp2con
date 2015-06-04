using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
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
}