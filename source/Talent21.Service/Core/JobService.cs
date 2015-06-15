using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class JobService : IJobService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IJobRepository _jobRepository;


        public JobService(IJobApplicationRepository jobApplicationRepository,
            IJobRepository jobRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _jobRepository = jobRepository;
        }
       
        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

      
        public CandidateJobViewModel ApplyToJob(CandidateJobViewModel job)
        {
            throw new System.NotImplementedException();
        }

        public CandidateAddJobViewModel CancelJob(CandidateAddJobViewModel job)
        {
            throw new System.NotImplementedException();
        }

        public CandidateRevokeJobModel RevokeJob(CandidateRevokeJobModel job)
        {
            throw new System.NotImplementedException();
        }
    }
}