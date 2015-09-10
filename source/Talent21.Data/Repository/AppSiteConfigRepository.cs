using System.Data.Entity;
using System.Linq;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IAppSiteConfigRepository : IRepository<AppSiteConfig>
    {
        AppSiteConfig Config();
    }

    public class AppSiteConfigRepository : EfRepository<AppSiteConfig>, IAppSiteConfigRepository
    {
        public AppSiteConfigRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppSiteConfig>().HasKey(x => x.Id);
        }

        public AppSiteConfig Config()
        {
            var config = All.FirstOrDefault();
            if (config == null)
            {
                config = new AppSiteConfig()
                {
                    Company = new AdvertisementPrice(),
                    Contractor = new AdvertisementPrice(),
                    Credit = new RateValidityConfig(),
                    Job = new AdvertisementPrice(),
                    Payment = new PaymentConfig(),
                    Tax = new TaxConfig(),
                    CompanyMembership = new RateValidityConfig(),
                    ContractorMembership = new RateValidityConfig()
                };
                Create(config);
                SaveChanges();
            }
            return config;
        }
    }
}