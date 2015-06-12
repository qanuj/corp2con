using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Plan : Dictionary
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
        public bool IsHidden { get; set; }
        public int MonthlyFee { get; set; }
        public int AnnualFee { get; set; }
    }
}