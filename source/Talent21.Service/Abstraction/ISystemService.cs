using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService
    {
        SystemAddIndustryModel AddIndustry(SystemAddIndustryModel model);
        SystemEditIndustryModel EditIndustry(SystemEditIndustryModel model);
        SystemDeleteIndustryModel DeleteIndustry(SystemDeleteIndustryModel model);
        SystemViewIndustryModel ViewIndustry(SystemViewIndustryModel model);
        SystemAddSkillModel AddSkill(SystemAddSkillModel model);
        SystemEditSkillModel EditSkill(SystemEditSkillModel model);
        SystemDeleteSkillModel DeleteSkill(SystemDeleteSkillModel model);
        SystemViewSkillModel ViewSkill(SystemViewSkillModel model);
    }
}