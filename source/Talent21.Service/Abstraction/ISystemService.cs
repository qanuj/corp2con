using Talent21.Service.Models;
using Talent21.Service.Models.Core;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService
    {
        AddIndustryViewModel AddIndustry(AddIndustryViewModel model);
        EditIndustryViewModel EditIndustry(EditIndustryViewModel model);
        DeleteIndustryViewModel DeleteIndustry(DeleteIndustryViewModel model);
        IndustryViewModel ViewIndustry(IndustryViewModel model);

        AddSkillViewModel AddSkill(AddSkillViewModel model);
        EditSkillViewModel EditSkill(EditSkillViewModel model);
        DeleteSkillViewModel DeleteSkill(DeleteSkillViewModel model);
        SkillViewModel ViewSkill(SkillViewModel model);

        LocationViewModel AddLocation(LocationCreateViewModel model);
    }
}