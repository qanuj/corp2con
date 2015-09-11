using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
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
}