using System.Linq;
using System.Threading.Tasks;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService,
        IDictionaryDataService<IndustryDictionaryEditViewModel, IndustryDictionaryCreateViewModel, IndustryDeleteViewModel>,
        IDictionaryDataService<FunctionalAreaDictionaryEditViewModel, FunctionalAreaDictionaryCreateViewModel, FunctionalAreaDeleteViewModel>,
        IDictionaryDataService<SkillDictionaryEditViewModel, SkillDictionaryCreateViewModel, SkillDeleteViewModel>,
        IDictionaryDataService<LocationDictionaryEditViewModel, LocationDictionaryCreateViewModel, LocationDeleteViewModel>
    {
        IQueryable<IndustryDictionaryViewModel> Industries { get; }
        IQueryable<FunctionalAreaDictionaryViewModel> FunctionalAreas { get; }
        IQueryable<LocationDictionaryViewModel> Locations { get; }
        IQueryable<SkillDictionaryViewModel> Skills { get; }

        string Upgrade();
    }
}