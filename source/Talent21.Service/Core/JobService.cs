using System;
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

        public bool CancelJob(CancelJobViewModel model)
        {
            var entity = _jobRepository.ById(model.JobId);
            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;
            _jobRepository.Update(entity);
            var rowAffected=_jobRepository.SaveChanges();
            return rowAffected>0;
        }

        public bool RevokeJobApplication(RevokeJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.JobApplicationId);
            entity.IsRevoked = true;
            entity.Revoked = DateTime.UtcNow;
            _jobApplicationRepository.Update(entity);
            var rowAffected = _jobRepository.SaveChanges();
            return rowAffected > 0;
        }
    }
}