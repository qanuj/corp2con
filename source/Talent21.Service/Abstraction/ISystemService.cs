namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService
    {
        SystemAddIndustryModel AddIndustry(SystemAddIndustryModel System);
        SystemEditIndustryModel EditIndustry(SystemEditIndustryModel System);
        SystemDeleteIndustryModel DeleteIndustry(SystemDeleteIndustryModel System);
        SystemViewIndustryModel ViewIndustry(SystemViewIndustryModel System);
        SystemAddSkillModel AddSkill(SystemAddSkillModel System);
        SystemEditSkillModel EditSkill(SystemEditSkillModel System);
        SystemDeleteSkillModel DeleteSkill(SystemDeleteSkillModel System);
        SystemViewSkillModel ViewSkill(SystemViewSkillModel System);
    }
}