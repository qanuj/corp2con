using System;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class JobService : IJobService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplicationRepository"></param>
        /// <param name="jobRepository"></param>
        /// <param name="_candidateRepository"></param>
        public JobService(IJobApplicationRepository jobApplicationRepository,
            IJobRepository jobRepository, ICandidateRepository _candidateRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _jobApplicationRepository.SaveChanges();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JobApplicationViewModel ApplyToJob(JobApplicationViewModel model)
        {
            var entity = new JobApplication
            {
                Act = JobActionEnum.Application,
                Id = model.Id,

            };
            _jobApplicationRepository.Create(entity);
            _jobApplicationRepository.SaveChanges();

            return new JobApplicationViewModel
            {
                Id = entity.Id,
                Act = entity.Act
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancelJob(CancelJobApplicationViewModel model)
        {
            var entity = _jobRepository.ById(model.JobId);
            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;
            _jobRepository.Update(entity);
            var rowAffected = _jobRepository.SaveChanges();
            return rowAffected > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RevokeJobApplication(RevokeJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.JobApplicationId);
            entity.IsRevoked = true;
            entity.Revoked = DateTime.UtcNow;
            _jobApplicationRepository.Update(entity);
            var rowAffected = _jobRepository.SaveChanges();
            return rowAffected > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICandidateRepository candidateRepository { get; set; }

        JobApplicationViewModel IJobService.ApplyToJob(JobApplicationViewModel job)
        {
            throw new NotImplementedException();
        }

        bool IJobService.CancelJob(CancelJobApplicationViewModel jobApplication)
        {
            throw new NotImplementedException();
        }

        ApplyJobApplicationViewModel IJobService.ApplyToJob(ApplyJobApplicationViewModel job)
        {
            throw new NotImplementedException();
        }

        bool IJobService.RevokeJobApplication(RevokeJobApplicationViewModel job)
        {
            throw new NotImplementedException();
        }
    }
}