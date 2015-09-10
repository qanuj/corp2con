using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class BenchInvite : Invite
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}