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
            get { return base.All.Include(x => x.Contractor).Include(x => x.History); }
        }

        public IQueryable<JobApplication> Mine(string userId)
        {
            return base.All
                .Include(x => x.History)
                .Include(x => x.Contractor)
                .Include(x => x.Contractor.Location)
                .Include(x => x.Job).Where(x => x.Job.Company.OwnerId == userId);
        }

        public Task<JobApplication> MineAsync(string userId, string profilepath)
        {
            return base.All.Include(x => x.Contractor).Include(x => x.Contractor.Location).Include(x => x.Job)
                .FirstOrDefaultAsync(x => x.Job.Company.OwnerId == userId && x.Contractor.ProfileUrl.EndsWith(profilepath));
        }


        public IQueryable<JobApplication> Contractor(string userId)
        {
            return base.All
                .Include(x => x.History)
                .Include(x => x.Contractor)
                .Include(x => x.Contractor.Location)
                .Include(x => x.Job).Where(x => x.Contractor.OwnerId == userId);
        }

        public Task<JobApplication> ContractorAsync(string userId, string profilepath)
        {
            return base.All.Include(x => x.Contractor).Include(x => x.Contractor.Location).Include(x => x.Job)
                .FirstOrDefaultAsync(x => x.Contractor.OwnerId == userId && x.Contractor.ProfileUrl.EndsWith(profilepath));
        }
    }

    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        IQueryable<JobApplication> Mine(string userId);
        Task<JobApplication> MineAsync(string userId, string profilepath);
        IQueryable<JobApplication> Contractor(string userId);
        Task<JobApplication> ContractorAsync(string userId, string profilepath);
    }

    public interface IJobApplicationHistoryRespository : IRepository<JobApplicationHistory> { }
    public class JobApplicationHistoryRespository : EfRepository<JobApplicationHistory>, IJobApplicationHistoryRespository
    {
        public JobApplicationHistoryRespository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        { }
    }
}
