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
            _candidateRepository = candidateRepository;
        }

        public int SaveChanges()
        {
            return _jobRepository.SaveChanges();
        }
       
        public JobApplictionViewModel ApplyToJob(JobApplictionViewModel model)
        {
            var entity = new JobApplication
            {
                Act = JobActionEnum.Application,
                Id = model.Id,
                
            };
            _jobApplicationRepository.Create(entity);
            _jobApplicationRepository.SaveChanges();
            return new JobApplictionViewModel
            {
                Id = entity.Id,
                Act = entity.Act
            };
        }

        public bool CancelJob(CancelJobViewModel model)
        {
            var entity = _jobRepository.ById(model.JobId);
            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;
            _jobRepository.Update(entity);
            var rowAffected = _jobRepository.SaveChanges();
            return rowAffected > 0;
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

        public ICandidateRepository candidateRepository { get; set; }
    }
}