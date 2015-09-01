using System;
using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using System.Linq;

namespace Talent21.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractorVisitRepository : EfRepository<ContractorVisit>, IContractorVisitRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public ContractorVisitRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        { 
        }

        public IQueryable<ContractorVisit> Mine(string userId)
        {
            return base.All.Where(x => x.Contractor.OwnerId == userId);
        }

        public bool VisitedEarlier(int id, string visitor)
        {
            var earlier = DateTime.UtcNow.AddMinutes(-30);
            return All.Any(x => x.Visitor == visitor && x.Created > earlier);
        }
    }

    public interface IContractorVisitRepository : IRepository<ContractorVisit>
    {
        IQueryable<ContractorVisit> Mine(string userId);
        bool VisitedEarlier(int id, string visitor);
    }
}