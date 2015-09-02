using System.Data.Entity;
using System.Linq;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IMemberRepository : IRepository<Company>
    {
        Member ByUserId(string userId);
    }

    public class MemberRepository : EfRepository<Company>, IMemberRepository
    {
        public MemberRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }

        public Member ByUserId(string userId)
        {
            return All.FirstOrDefault(x => x.OwnerId == userId);
        }
    }
}