namespace Talent21.Data.Core
{
    public class CandidateSkill : SkillExtended
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }
}