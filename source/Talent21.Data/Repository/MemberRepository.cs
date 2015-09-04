using System.Data.Entity;
using System.Linq;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IMemberRepository : IRepository<Member>
    {
        Member ByUserId(string userId);
    }

    public class MemberRepository : EfRepository<Member>, IMemberRepository
    {
        public MemberRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }

        public Member ByUserId(string userId)
        {
            return All.Include(x => x.Location).FirstOrDefault(x => x.OwnerId == userId);
        }
    }
}