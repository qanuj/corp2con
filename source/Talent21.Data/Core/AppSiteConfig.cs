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

        public AppSiteConfig()
        {
            Company = new AdvertisementPrice();
            Contractor = new AdvertisementPrice();
            Credit = new RateValidityConfig();
            Job = new AdvertisementPrice();
            Payment = new PaymentConfig();
            Tax = new TaxConfig();
            CompanyMembership = new RateValidityConfig();
            ContractorMembership = new RateValidityConfig();
            JobPrice=new RateValidityConfig();
        }
    }
}