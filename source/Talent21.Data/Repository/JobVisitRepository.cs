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
    /// <summary>
    /// 
    /// </summary>
    public class JobVisitRepository : EfRepository<JobVisit>, IJobVisitRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public JobVisitRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        { }

        public bool VisitedEarlier(int id,string ip, string visitor)
        {
            var earlier = DateTime.UtcNow.AddMinutes(-30);
            if (string.IsNullOrWhiteSpace(visitor)) return false;
            return All.Any(x => x.Visitor == visitor && x.Created > earlier && x.JobId == id && x.IpAddress==ip);
        }

        public IQueryable<JobVisit> Mine(string userId)
        {
            return All.Where(x => x.Job.Company.OwnerId == userId);
        }

        public IQueryable<JobVisit> ByJobId(IQueryable<int> jobIds)
        {
            return All.Where(x => jobIds.Contains(x.JobId));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IJobVisitRepository : IRepository<JobVisit>
    {
        bool VisitedEarlier(int id,string ip, string visitor);
        IQueryable<JobVisit> Mine(string userId);
        IQueryable<JobVisit> ByJobId(IQueryable<int> jobIds);
    }
}
