using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Security.Cryptography.X509Certificates;

namespace e10.Shared.Data.Abstraction
{
    public class Conversation : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    [ComplexType]
    public class RateValidityConfig
    {
        public int Rate { get; set; }
        public int Validity { get; set; }
    }

    [ComplexType]
    public class TaxConfig
    {
        public string Name { get; set; }
        public double Rate { get; set; }
    }

    [ComplexType]
    public class PaymentConfig
    {
        public string Key { get; set; }
        public string Salt { get; set; }
        public string MerchantId { get; set; }
        public string Url { get; set; }
    }


    [ComplexType]
    public class AdvertisementPrice
    {
        public RateValidityConfig Highlight { get; set; }
        public RateValidityConfig Featured { get; set; }
        public RateValidityConfig Global { get; set; }
        public RateValidityConfig Advertise { get; set; }

        public AdvertisementPrice()
        {
            this.Highlight = new RateValidityConfig();
            this.Global = new RateValidityConfig();
            this.Featured = new RateValidityConfig();
            this.Advertise = new RateValidityConfig();
        }
    }

    public abstract class SiteConfig : Entity
    {
        public PaymentConfig Payment { get; set; }
        public RateValidityConfig Credit { get; set; }
        public TaxConfig Tax { get; set; }
    }
}