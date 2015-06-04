using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Block : Entity
    {
        public CompanyOrCandidateEnum BlockedBy { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }
}