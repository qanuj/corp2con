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

    public class JobApplicationRepository : EfRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        { }

        public override IQueryable<JobApplication> All
        {
            get { return base.All.Include(x => x.Candidate).Include(x => x.History); }
        }
    }

    public interface IJobApplicationRepository : IRepository<JobApplication>
    {

    }
}
