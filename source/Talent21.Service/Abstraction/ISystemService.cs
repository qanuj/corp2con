using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISystemService : IService,
        IDictionaryDataService<IndustryDictionaryEditViewModel, IndustryDictionaryCreateViewModel, IndustryDeleteViewModel>,
        IDictionaryDataService<FunctionalAreaDictionaryEditViewModel, FunctionalAreaDictionaryCreateViewModel, FunctionalAreaDeleteViewModel>,
        IDictionaryDataService<SkillDictionaryEditViewModel, SkillDictionaryCreateViewModel, SkillDeleteViewModel>,
        IDictionaryDataService<CountryDictionaryEditViewModel, CountryDictionaryCreateViewModel, CountryDeleteViewModel>,
        IDictionaryDataService<LocationDictionaryEditViewModel, LocationDictionaryCreateViewModel, LocationDeleteViewModel>
    {
        IQueryable<IndustryDictionaryViewModel> Industries { get; }
        IQueryable<FunctionalAreaDictionaryViewModel> FunctionalAreas { get; }
        IQueryable<LocationDictionaryViewModel> Locations { get; }
        IQueryable<CountryDictionaryViewModel> Countries { get; }
        IQueryable<SkillDictionaryViewModel> Skills { get; }

        string Upgrade();
        EnumList Enums();

        IQueryable<Transaction> Transactions(bool includeUser);
        InvoiceViewModel TransactionById(int id);

        string Hash(string email);
        bool SendGift(GiftViewModel model);
        IQueryable<TransactionViewModel> AllTransactions();
        AppSiteConfig UpdateConfig(AppSiteConfig model);
        AppSiteConfig GetOrCreateConfig();
        IQueryable<FeedbackViewModel> Feedbacks();
        bool DeleteFeedback(int id);
        bool ReadFeedback(int id, bool what);
    }
}