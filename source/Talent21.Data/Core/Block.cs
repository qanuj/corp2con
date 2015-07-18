using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Block : Entity
    {
        public CompanyOrContractorEnum BlockedBy { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }
    }
}