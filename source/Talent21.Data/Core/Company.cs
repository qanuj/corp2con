using System.Collections.Generic;

namespace Talent21.Data.Core
{
    public class Company : Person
    {
        public Industry Industry { get; set; }
        public int? IndustryId { get; set; }

        public IList<ContractSchedule> Schedules { get; set; }
        public IList<CompanyVisit> Visits { get; set; }
        public IList<Job> Jobs { get; set; }
    }
}