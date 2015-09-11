namespace e10.Shared.Data.Abstraction
{
    public abstract class SiteConfig : Entity
    {
        public PaymentConfig Payment { get; set; }
        public RateValidityConfig Credit { get; set; }
        public TaxConfig Tax { get; set; }
    }
}