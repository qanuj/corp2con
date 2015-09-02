using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class ContractorVisit : Visit
    {
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }
    }
}