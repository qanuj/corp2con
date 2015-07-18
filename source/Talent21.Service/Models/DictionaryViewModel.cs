using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class DictionaryViewModel  
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
    public class DictionaryEditViewModel : DictionaryViewModel
    {
        public int Id { get; set; }
    }

    public class ContractorSkillViewModel : DictionaryViewModel
    {
        public LevelEnum Level { get; set; }
        public ProficiencyEnum Proficiency { get; set; }
        public int ExperienceInMonths { get; set; }
    }
}