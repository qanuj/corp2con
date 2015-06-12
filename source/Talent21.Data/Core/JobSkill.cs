namespace Talent21.Data.Core
{
    public class JobSkill : SkillExtended
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }
}