using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

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
    }
}