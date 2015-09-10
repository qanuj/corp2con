using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class AppSiteConfig : SiteConfig
    {
        public AdvertisementPrice Contractor { get; set; }
        public AdvertisementPrice Company { get; set; }
        public AdvertisementPrice Job { get; set; }
        public RateValidityConfig JobPrice { get; set; }
        public RateValidityConfig ContractorMembership { get; set; }
        public RateValidityConfig CompanyMembership { get; set; }
    }
}