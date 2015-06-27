using System;
using System.Linq;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IScheduleRepository _scheduleRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateRepository"></param>
        /// <param name="jobApplicationRepository"></param>
        /// <param name="scheduleRepository"></param>
        public CandidateService(ICandidateRepository candidateRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository)
        {
            _candidateRepository = candidateRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _scheduleRepository = scheduleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public CreateCandidateViewModel CreateCandidate(CreateCandidateViewModel profile)
        {
            var candidate = new Candidate() { Name = profile.Name, OwnerId = profile.UserId };
            _candidateRepository.Create(candidate);
            _candidateRepository.SaveChanges();
            return new CreateCandidateViewModel
            {
                Name = candidate.Name,
                Id = candidate.Id
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JobApplicationViewModel CreateCandidateAndApplyToJob(
            CreateCandidateAndApplyToJobViewModel model)
        {
            var candiate = CreateCandidate(new CreateCandidateViewModel()
            {
                Name = model.Name
            });
            return ApplyToJob(new ApplyJobApplicationViewModel
            {
                CandidateId = candiate.Id,
                JobId = model.JobId
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public JobApplicationViewModel ApplyToJob(ApplyJobApplicationViewModel job)
        {
            var jobApplication = new JobApplication
            {
                Act = JobActionEnum.Application,
                CandidateId = job.CandidateId,
                JobId = job.JobId,
            };
            _jobApplicationRepository.Create(jobApplication);
            _jobApplicationRepository.SaveChanges();
            return new JobApplicationViewModel
            {
                Id = jobApplication.Id,
                Act = jobApplication.Act
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            candidate.Name = profile.Name;
            candidate.LocationId = profile.LocationId;
            candidate.Email = profile.Email;
            _candidateRepository.Update(candidate);
            _candidateRepository.SaveChanges();
            return profile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AddScheduleViewModel AddSchedule(AddScheduleViewModel model)
        {

            var schedule = new Schedule
            {
                Start = model.Start,
                End = model.End

            };
            _scheduleRepository.Create(schedule);
            _scheduleRepository.SaveChanges();
            return new AddScheduleViewModel
            {
                CandidateId = schedule.CandidateId,
                Start = schedule.Start,
                End = schedule.End
            };


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CreateScheduleViewModel CreateSchedule(CreateScheduleViewModel model)
        {
            var entity = new Schedule() { CandidateId = model.CandidateId };
            _scheduleRepository.Create(entity);
            _scheduleRepository.SaveChanges();
            return new CreateScheduleViewModel
            {
                Id = entity.Id,
                CandidateId = entity.CandidateId,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public bool DeleteProfile(DeleteProfileViewModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            _candidateRepository.Delete(candidate);
            return _candidateRepository.SaveChanges() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UpdateScheduleViewModel UpdateSchedule(UpdateScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.CandidateId);
            entity.Start = model.Start;
            entity.End = model.End;
            _scheduleRepository.Update(entity);
            _scheduleRepository.SaveChanges();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DeleteScheduleViewModel DeleteSchedule(DeleteScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.CandidateId);
            _scheduleRepository.Delete(entity);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ScheduleViewModel ViewSchedule(ScheduleViewModel model)
        {
            throw new System.NotImplementedException();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CandidatePublicProfileViewModel GetProfile(int id)
        {
            var candidate = _candidateRepository.ById(id);
            if(candidate == null) return null;
            return new CandidatePublicProfileViewModel()
            {
                Id = candidate.Id,
                Name = candidate.Name
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CandidatePublicProfileViewModel> GetProfileQuery()
        {
            var query = from row in _candidateRepository.All
                        select new CandidatePublicProfileViewModel
                        {
                            Id = row.Id,
                            Name = row.Name
                        };
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScheduleViewModel GetSchedule(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<ScheduleViewModel> GetSchedules()
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(IdModel model)
        {
            _candidateRepository.Delete(model.Id);
            var rowsAffested = _candidateRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public ContractorEditViewModel Create(ContractorCreateViewModel model)
        {
            var entity = new Candidate
            {
                Name = model.Name,
                Email = model.Email
            };
            _candidateRepository.Create(entity);
            _candidateRepository.SaveChanges();
            return new ContractorEditViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
        }

        public ContractorEditViewModel Update(ContractorEditViewModel model)
        {
            var entity = _candidateRepository.ById(model.Id);
            if(entity == null) return null;

            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.About = model.About;
            entity.Experience=new Duration(){Months = model.ExperienceMonths,Years = model.ExperienceYears};
            entity.LocationId = model.LocationId;
            entity.Mobile = model.Mobile;
            entity.Social = new Social
            {
                Twitter = model.Twitter,
                Facebook = model.Facebook,
                Yahoo = model.Yahoo,
                Google = model.Google,
                LinkedIn = model.LinkedIn,
                Rss = model.Rss,
                WebSite = model.WebSite
            };

            _candidateRepository.Update(entity);
            _candidateRepository.SaveChanges();

            return model;
        }
    }
}
