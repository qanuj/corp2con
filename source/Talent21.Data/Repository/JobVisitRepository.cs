using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public class JobVisitRepository : EfRepository<JobVisit>, IJobVisitRepository
    {
        public JobVisitRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        { }
    }
    public interface IJobVisitRepository : IRepository<JobVisit>
    { }
}
