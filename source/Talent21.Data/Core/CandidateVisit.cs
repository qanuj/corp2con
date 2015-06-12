namespace Talent21.Data.Core
{
    public class CandidateVisit : Visit
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }
}