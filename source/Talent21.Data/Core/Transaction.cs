using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Transaction : Entity
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }
}