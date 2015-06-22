using Talent21.Service.Models;
using Talent21.Service.Models.Core;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AddIndustryViewModel AddIndustry(AddIndustryViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EditIndustryViewModel EditIndustry(EditIndustryViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DeleteIndustryViewModel DeleteIndustry(DeleteIndustryViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IndustryViewModel ViewIndustry(IndustryViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AddSkillViewModel AddSkill(AddSkillViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EditSkillViewModel EditSkill(EditSkillViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DeleteSkillViewModel DeleteSkill(DeleteSkillViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SkillViewModel ViewSkill(SkillViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        LocationViewModel AddLocation(LocationCreateViewModel model);

    }
}