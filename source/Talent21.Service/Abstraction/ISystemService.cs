using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IDictionaryDataService<TEditModel, TCreateModel, TDeleteModel>
        where TCreateModel : DictionaryViewModel
        where TEditModel : EditDictionaryViewModel
        where TDeleteModel : IdModel
    {
        bool Delete(TDeleteModel model);
        TEditModel Create(TCreateModel model);
        TEditModel Update(TEditModel model);
    }

    public interface ISystemService : IService,
        IDictionaryDataService<IndustryEditViewModel, IndustryCreateViewModel, IndustryDeleteViewModel>,
        IDictionaryDataService<SkillEditViewModel, SkillCreateViewModel, SkillDeleteViewModel>,
        IDictionaryDataService<LocationEditViewModel, LocationCreateViewModel, LocationDeleteViewModel>
    {
        IQueryable<IndustryViewModel> Industries { get; }
        IQueryable<LocationViewModel> Locations { get; }
        IQueryable<SkillViewModel> Skills { get; }
    }


}