using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class RateValidityConfig
    {
        public int Rate { get; set; }
        public int Validity { get; set; }
    }
}