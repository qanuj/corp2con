using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public class FunctionalAreaRepository : EfRepository<FunctionalArea>, IFunctionalAreaRepository
    {
        public FunctionalAreaRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
    }

    public interface IFunctionalAreaRepository : IRepository<FunctionalArea>
    {
    }
}