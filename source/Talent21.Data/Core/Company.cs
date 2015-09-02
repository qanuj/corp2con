using System.Collections.Generic;

namespace Talent21.Data.Core
{
    public class Company : Member
    {
        public string CompanyName { get; set; }

        public OrganizationTypeEnum OrganizationType { get; set; }

        public IList<ContractSchedule> Schedules { get; set; }
        public IList<CompanyVisit> Visits { get; set; }
        public IList<CompanyAdvertisement> Advertisements { get; set; }
        public ICollection<ContractorFolder> Folders { get; set; }
        public ICollection<Contractor> Contractors { get; set; }

        public IList<Contact> Team { get; set; }

        public IList<Job> Jobs { get; set; }

    }
}