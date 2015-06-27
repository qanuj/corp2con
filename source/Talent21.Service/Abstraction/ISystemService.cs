using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService,
        IDictionaryDataService<IndustryDictionaryEditViewModel, IndustryDictionaryCreateViewModel, IndustryDeleteViewModel>,
        IDictionaryDataService<SkillDictionaryEditViewModel, SkillDictionaryCreateViewModel, SkillDeleteViewModel>,
        IDictionaryDataService<LocationDictionaryEditViewModel, LocationDictionaryCreateViewModel, LocationDeleteViewModel>
    {
        IQueryable<IndustryDictionaryViewModel> Industries { get; }
        IQueryable<LocationDictionaryViewModel> Locations { get; }
        IQueryable<SkillDictionaryViewModel> Skills { get; }
    }
}