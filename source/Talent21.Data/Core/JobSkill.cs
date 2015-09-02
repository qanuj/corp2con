using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class JobSkill : Entity
    {
        public Job Job { get; set; }
        public int JobId { get; set; }

        public Skill Skill { get; set; }
        public int SkillId { get; set; }

        public LevelEnum Level { get; set; }
    }
}