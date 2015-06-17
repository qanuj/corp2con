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
        private readonly ICandidateRepository _candidateRepository;


        public JobService(IJobApplicationRepository jobApplicationRepository,
            IJobRepository jobRepository, ICandidateRepository _candidateRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _jobRepository = jobRepository;
        }
       
        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }


        public CandidateJobViewModel ApplyToJob(CandidateJobViewModel model)
        {
            throw new System.NotImplementedException();
            //var entity = new Entity
            //{
            //    Act = JobActionEnum.Application,
            //    CandidateId = model.CandidateId,
            //    JobId = model.JobId,
            //};
            //_jobRepository.Create(entity);
            //_candidateRepository.SaveChanges();
            //return new CandidateJobViewModel
            //{
            //    CandidateId = entity.CandidateId,
            //    JobId = entity.JobId
            //};
        }

        public CandidateAddJobViewModel CancelJob(CandidateAddJobViewModel model)
        {
            throw new System.NotImplementedException();
            //var entity = _jobRepository.ById(model.CompanyId);
            //_jobRepository.Cancel(entity);
            //return model;
        }

        public CandidateRevokeJobModel RevokeJob(CandidateRevokeJobModel model)
        {
            throw new System.NotImplementedException();
            //var entity = _jobRepository.ById(model.JobId);
            //_jobRepository.Revoke(entity);
            //return model;
        }


       
    }
}