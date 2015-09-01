using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IJobApplicationHistoryRespository : IRepository<JobApplicationHistory> { }
    public class JobApplicationHistoryRespository : EfRepository<JobApplicationHistory>, IJobApplicationHistoryRespository
    {
        public JobApplicationHistoryRespository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        { }
    }
}