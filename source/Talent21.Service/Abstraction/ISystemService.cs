using Talent21.Service.Models;
using Talent21.Service.Models.Core;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService
    {
        IndustryAddViewModel AddIndustry(IndustryAddViewModel model);
        IndustryEditViewModel EditIndustry(IndustryEditViewModel model);
        IndustryDeleteViewModel DeleteIndustry(IndustryDeleteViewModel model);
        IndustryViewModel ViewIndustry(IndustryViewModel model);

        SkillAddViewModel AddSkill(SkillAddViewModel model);
        SkillEditViewModel EditSkill(SkillEditViewModel model);
        SkillDeleteViewModel DeleteSkill(SkillDeleteViewModel model);
        SkillViewModel ViewSkill(SkillViewModel model);

        LocationViewModel AddLocation(LocationCreateViewModel model);
    }
}