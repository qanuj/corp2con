using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class CandidateService : SharedService, ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public CandidateService(ICandidateRepository candidateRepository,
            IJobRepository jobRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository)
            : base(jobRepository)
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

        public IQueryable<JobApplicationViewModel> Applications(int id)
        {
            return _jobApplicationRepository.All.Where(x => x.JobId == id).Select(x => new JobApplicationViewModel
            {
                Actions = x.History.Select(y => new JobApplicationHistoryViewModel() { Act = y.Act, Created = y.Created, CreateBy = y.CreatedBy }),
                Id = x.Id,
                Contractor = new ContractorViewModel
                {
                    Id = x.Candidate.Id,
                    About = x.Candidate.About,
                    Email = x.Candidate.Email,
                    ExperienceMonths = x.Candidate.Experience.Months,
                    ExperienceYears = x.Candidate.Experience.Years,
                    Facebook = x.Candidate.Social.Facebook,
                    Google = x.Candidate.Social.Google,
                    LinkedIn = x.Candidate.Social.LinkedIn,
                    LocationId = x.Candidate.LocationId,
                    Mobile = x.Candidate.Mobile,
                    Name = x.Candidate.Name,
                    Rss = x.Candidate.Social.Rss,
                    Twitter = x.Candidate.Social.Twitter,
                    WebSite = x.Candidate.Social.WebSite,
                    Yahoo = x.Candidate.Social.Yahoo,
                    PictureUrl = x.Candidate.PictureUrl,
                    OwnerId = x.Candidate.OwnerId,
                    Rate = x.Candidate.Rate,
                    Skills = x.Candidate.Skills.Select(y => new DictionaryViewModel() { Code = y.Code, Title = y.Title })
                }
            });
        }

        public IQueryable<ScheduleViewModel> Schedules
        {
            get
            {
                return _scheduleRepository.All.Where(x => x.Candidate.OwnerId == CurrentUserId).Select(x => new ScheduleViewModel
                {
                    Id = x.Id, //TODO: Add more fields.
                });
            }
        }

        private Candidate FindCandidate(string userId)
        {
            return _candidateRepository.All.FirstOrDefault(x => x.OwnerId == userId);
        }

        public bool Delete(DeleteProfileViewModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            _candidateRepository.Delete(candidate);
            return _candidateRepository.SaveChanges() > 0;
        }

        public ContractorViewModel GetProfile(string userId)
        {
            return Contractors.FirstOrDefault(x => x.OwnerId == userId);
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

        public ScheduleViewModel Update(EditScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.Id);
            if(entity == null) throw new Exception("Schedule not found");

            entity.Start = model.Start;
            entity.End = model.End;

            _scheduleRepository.Update(entity);
            _scheduleRepository.SaveChanges();

            return Schedules.FirstOrDefault(x => x.Id == entity.Id);
        }

        public bool Delete(DeleteScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.Id);
            _scheduleRepository.Delete(entity);
            return _scheduleRepository.SaveChanges() > 0;
        }

        public ScheduleViewModel Create(CreateScheduleViewModel model)
        {
            var candidate = FindCandidate(CurrentUserId);
            var entity = new Schedule
            {
                CandidateId = candidate.Id,
                Start = model.Start,
                End = model.End
            };
            _scheduleRepository.Create(entity);
            _scheduleRepository.SaveChanges();

            return Schedules.FirstOrDefault(x => x.Id == entity.Id);
        }


        public bool ActOnApplication(CompanyActJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if(entity == null) return false;

            entity.History.Add(new JobApplicationHistory() { Act = model.Act, CreatedBy = CurrentUserId });

            var rowsAffested = _jobApplicationRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act)
        {
            return ActOnApplication(new CompanyActJobApplicationViewModel(model, act));
        }

        public JobApplicationViewModel Apply(JobApplicationCreateViewModel model)
        {
            var candidate = FindCandidate(CurrentUserId);

            var jobApplication = new JobApplication
            {
                CandidateId = candidate.CandidateId,
                JobId = model.Id,
            };
            var history = new JobApplicationHistory() { Act = JobActionEnum.Application };
            jobApplication.History.Add(history);

            _jobApplicationRepository.Create(jobApplication);
            _jobApplicationRepository.SaveChanges();

            return Applications(jobApplication.JobId).FirstOrDefault(x => x.Id == jobApplication.Id);
        }
    }
}