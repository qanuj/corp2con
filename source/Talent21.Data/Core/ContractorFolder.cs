using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class ContractorFolder : Entity
    {
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public string Folder { get; set; }
    }
}