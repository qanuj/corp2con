using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;

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
        public int Highlight { get; set; }
        public int Featured { get; set; }
        public int Global { get; set; }
        public int Advertise { get; set; }
    }

    public abstract class SiteConfig : Entity
    {
        public PaymentConfig Payment { get; set; }
        public RateValidityConfig Credit { get; set; }
        public TaxConfig Tax { get; set; }
    }
}