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
        private readonly IScheduleRepository _scheduleRepository;

        public CandidateService(ICandidateRepository candidateRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository)
        {
            _candidateRepository = candidateRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _scheduleRepository = scheduleRepository;
        }

        public CreateCandidateViewModel CreateCandidate(CreateCandidateViewModel profile)
        {
            var candidate = new Candidate() { Name = profile.Name };
            _candidateRepository.Create(candidate);
            _candidateRepository.SaveChanges();
            return new CreateCandidateViewModel
            {
                Name = candidate.Name,
                Id = candidate.Id
            };
        }

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


        public bool DeleteProfile(DeleteProfileViewModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            _candidateRepository.Delete(candidate);
            return _candidateRepository.SaveChanges() > 0;
        }

        public UpdateScheduleViewModel UpdateSchedule(UpdateScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.CandidateId);
            entity.Start = model.Start;
            entity.End = model.End;
            _scheduleRepository.Update(entity);
            _scheduleRepository.SaveChanges();
            return model;
        }


        public DeleteScheduleViewModel DeleteSchedule(DeleteScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.CandidateId);
            _scheduleRepository.Delete(entity);
            return model;
        }

        public ScheduleViewModel ViewSchedule(ScheduleViewModel model)
        {
            throw new System.NotImplementedException();

        }


    }
}
