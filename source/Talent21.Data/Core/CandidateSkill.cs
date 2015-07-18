namespace Talent21.Data.Core
{
    public class CandidateSkill
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public Skill Skill { get; set; }
        public int SkillId { get; set; }

        public ProficiencyEnum Proficiency { get; set; }
        public LevelEnum Level { get; set; }

        public int ExperienceInMonths { get; set; }
    }
}