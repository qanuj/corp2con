using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        public CandidateService(ICandidateRepository candidateRepository,IJobApplicationRepository jobApplicationRepository)
        {
            _candidateRepository = candidateRepository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public CandidateViewModel CreateCandidate(CandidateCreateViewModel profile)
        {
            var candidate = new Candidate() { Name = profile.Name };
            _candidateRepository.Create(candidate);
            _candidateRepository.SaveChanges();
            return new CandidateViewModel
            {
                Name = candidate.Name,
                CandidateId = candidate.Id
            };
        }

        public JobApplictionViewModel CreateCandidateAndApplyToJob(
            CreateCandidateAndApplyToJobViewModel model)
        {
            var candiate=CreateCandidate(new CandidateCreateViewModel()
            {
                Name = model.Name
            });
            return ApplyToJob(new JobApplictionCreateViewModel
            {
                CandidateId = candiate.CandidateId,
                JobId = model.JobId
            });
        }
        public JobApplictionViewModel ApplyToJob(JobApplictionCreateViewModel job)
        {
            var jobApplication = new JobApplication
            {
                Act = JobActionEnum.Application,
                CandidateId = job.CandidateId,
                JobId=job.JobId,
            };
            _jobApplicationRepository.Create(jobApplication);
            _candidateRepository.SaveChanges();
            return new JobApplictionViewModel
            {
                Id = jobApplication.Id,
                Act = jobApplication.Act
            };
        }
        public CandidateProfileViewModel UpdateProfile(CandidateProfileViewModel profile)
        {
            var candidate=_candidateRepository.ById(profile.Id);
            candidate.Name = profile.Name;
            candidate.LocationId = profile.LocationId;
            candidate.Email = profile.Email;
            _candidateRepository.Update(candidate);
            _candidateRepository.SaveChanges();
            return profile;
        }

        public ScheduleCreateViewModel CreateSchedule(ScheduleCreateViewModel schedule)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteProfile(CandidateDeleteProfileModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            _candidateRepository.Delete(candidate);
            return _candidateRepository.SaveChanges()>0;
        }

        public CandidateAddScheduleModel AddSchedule(CandidateAddScheduleModel schedule)
        {
            throw new System.NotImplementedException();
        }

        public CandidateUpdateScheduleModel UpdateSchedule(CandidateUpdateScheduleModel schedule)
        {
            throw new System.NotImplementedException();
        }

        public CandidateDeleteScheduleModel DeleteSchedule(CandidateDeleteScheduleModel schedule)
        {
            throw new System.NotImplementedException();
        }

        public CandidateViewScheduleModel ViewSchedule(CandidateViewScheduleModel schedule)
        {
            throw new System.NotImplementedException();
        }
    }
}
