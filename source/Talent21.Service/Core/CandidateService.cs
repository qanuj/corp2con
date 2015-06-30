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

        public CandidateService(ICandidateRepository candidateRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository)
        {
            _candidateRepository = candidateRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _scheduleRepository = scheduleRepository;
        }

        public IQueryable<ContractorViewModel> Contractors
        {
            get
            {
                return _candidateRepository.All.Select(x => new ContractorViewModel
                {
                    Id = x.Id,
                    About = x.About,
                    Email = x.Email,
                    ExperienceMonths = x.Experience.Months,
                    ExperienceYears = x.Experience.Years,
                    Facebook = x.Social.Facebook,
                    Google = x.Social.Google,
                    LinkedIn = x.Social.LinkedIn,
                    LocationId = x.LocationId,
                    Mobile = x.Mobile,
                    Name = x.Name,
                    Rss = x.Social.Rss,
                    Twitter = x.Social.Twitter,
                    WebSite = x.Social.WebSite,
                    Yahoo = x.Social.Yahoo,
                    PictureUrl = x.PictureUrl,
                    OwnerId = x.OwnerId,
                    Rate = x.Rate,
                    Skills = x.Skills.Select(y => new DictionaryViewModel() { Code = y.Code, Title = y.Title })
                });
            }
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

        public ContractorViewModel GetProfile(string userId)
        {
            return Contractors.FirstOrDefault(x => x.OwnerId == userId);
        }


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

        public ScheduleViewModel GetSchedule(int id)
        {
            throw new System.NotImplementedException();
        }

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
                OwnerId = model.OwnerId,
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
            entity.Experience = new Duration() { Months = model.ExperienceMonths, Years = model.ExperienceYears };
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


        public IQueryable<ScheduleViewModel> Schedules(int contractorId)
        {
            return _scheduleRepository.All.Where(x=>x.CandidateId==contractorId).Select(x => new ScheduleViewModel
            {
                Id = x.Id, //TODO: Add more fields.
            });
        }
    }
}