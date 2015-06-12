namespace Talent21.Data.Core
{
    public class SkillExtended : Skill
    {
        public Skill Skill { get; set; }
        public LevelEnum Level { get; set; }
    }
}