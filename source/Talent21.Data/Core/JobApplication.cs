using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class JobApplication : Entity
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public Job Job { get; set; }
        public int JobId { get; set; }

        public JobActionEnum Act { get; set; }

        public bool IsRevoked { get; set; }

        public System.DateTime Revoked { get; set; }
    }
}