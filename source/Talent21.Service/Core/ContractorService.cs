using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class ContractorService : SharedService, IContractorService
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IContractorVisitRepository _contractorVisitRepository;
        private readonly IContractorSkillRepository _contractorSkillRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISkillRepository _skillRepository;

        public ContractorService(IContractorRepository contractorRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository,
            ISkillRepository skillRepository,
            IContractorSkillRepository contractorSkillRepository,
            ILocationRepository locationRepository,
            IContractorVisitRepository contractorVisitRepository,
            IJobRepository jobRepository)
            : base(locationRepository)
        {
            _contractorRepository = contractorRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _scheduleRepository = scheduleRepository;
            _skillRepository = skillRepository;
            _contractorSkillRepository = contractorSkillRepository;
            _contractorVisitRepository = contractorVisitRepository;
            _jobRepository = jobRepository;
        }

        public IQueryable<ContractorViewModel> Contractors
        {
            get
            {
                var query = from x in _contractorRepository.All
                            select new ContractorViewModel
                            {
                                Id = x.Id,
                                About = x.About,
                                Email = x.Email,
                                Nationality = x.Nationality,
                                AlternateNumber = x.AlternateNumber,
                                ConsultantType = x.ConsultantType,
                                ContractType = x.ContractType,
                                Gender = x.Gender,
                                Profile = x.Profile,
                                FunctionalAreaId = x.FunctionalAreaId,
                                ExperienceMonths = x.Experience.Months,
                                ExperienceYears = x.Experience.Years,
                                Facebook = x.Social.Facebook,
                                Google = x.Social.Google,
                                LinkedIn = x.Social.LinkedIn,
                                Location = x.Location.Title,
                                Mobile = x.Mobile,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Rss = x.Social.Rss,
                                Twitter = x.Social.Twitter,
                                WebSite = x.Social.WebSite,
                                Yahoo = x.Social.Yahoo,
                                PictureUrl = x.PictureUrl,
                                OwnerId = x.OwnerId,
                                Rate = x.Rate,
                                Skills = _contractorSkillRepository.All.Where(y=>y.ContractorId==x.Id).Select(y => new ContractorSkillViewModel()
                                {
                                    Id = y.Id,
                                    Code = y.Skill.Code,
                                    Title = y.Skill.Title,
                                    ExperienceInMonths = y.ExperienceInMonths,
                                    Level = y.Level,
                                    Proficiency = y.Proficiency
                                })
                            };
                return query;
            }
        }

        public IQueryable<ContractorSkillViewModel> Skills
        {
            get
            {
                return _contractorRepository.All.SelectMany(x => x.Skills.Select(y => new ContractorSkillViewModel
                {
                    Id = y.Skill.Id,
                    Code = y.Skill.Code,
                    Title = y.Skill.Title,
                    ExperienceInMonths = y.ExperienceInMonths,
                    Level = y.Level,
                    Proficiency = y.Proficiency
                }));
            }
        }

        public IQueryable<JobApplicationContractorViewModel> Applications(int id = 0)
        {
            return _jobApplicationRepository.All.Where(x => x.JobId == id || id == 0).Select(x => new JobApplicationContractorViewModel
            {
                Actions = x.History.Select(y => new JobApplicationHistoryViewModel() { Act = y.Act, Created = y.Created, CreateBy = y.CreatedBy }),
                Id = x.Id,
                Job = new JobViewModel
                {
                    Id = x.Job.Id,
                    Company = x.Job.Company.CompanyName,
                    IsCancelled = x.Job.IsCancelled,
                    Cancelled = x.Job.Cancelled,
                    Published = x.Job.Published,
                    Location = x.Job.Location.Title,
                    Skills = x.Job.Skills.Select(y => new JobSkillEditViewModel { Code = y.Skill.Code, Id = y.Id, Title = y.Skill.Title, Level = y.Level}),
                    CompanyId = x.Job.CompanyId,
                    Description = x.Job.Description,
                    Code = x.Job.Code,
                    Title = x.Job.Title,
                    End = x.Job.End,
                    Rate = x.Job.Rate,
                    Start = x.Job.Start
                }
            });
        }

        public IQueryable<ScheduleViewModel> Schedules
        {
            get
            {
                return _scheduleRepository.All.Where(x => x.Contractor.OwnerId == CurrentUserId).Select(x => new ScheduleViewModel
                {
                    Id = x.Id,
                    Start = x.Start,
                    End = x.End,
                    Company = x.Description,
                    IsAvailable = x.IsAvailable
                });
            }
        }

        private Contractor FindContractor(string userId)
        {
            return _contractorRepository.All.FirstOrDefault(x => x.OwnerId == userId);
        }

        public bool Delete(DeleteProfileViewModel profile)
        {
            var contractor = _contractorRepository.ById(profile.Id);
            _contractorRepository.Delete(contractor);
            return _contractorRepository.SaveChanges() > 0;
        }

        public ContractorViewModel GetProfile(string userId)
        {
            return Contractors.FirstOrDefault(x => x.OwnerId == userId);
        }
        public ContractorViewModel GetProfile(int id)
        {
            return Contractors.FirstOrDefault(x => x.Id == id);
        }

        public bool Delete(IdModel model)
        {
            _contractorRepository.Delete(model.Id);
            var rowsAffested = _contractorRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public ContractorEditViewModel Create(ContractorCreateViewModel model)
        {
            var entity = new Contractor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                OwnerId = model.OwnerId,
                Email = model.Email
            };
            _contractorRepository.Create(entity);
            _contractorRepository.SaveChanges();
            return new ContractorEditViewModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }

        public ContractorEditViewModel Update(ContractorEditViewModel model)
        {
            var entity = _contractorRepository.ById(model.Id);
            if(entity == null) return null;

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.PictureUrl = model.PictureUrl;
            entity.Email = model.Email;
            entity.About = model.About;
            entity.Rate = model.Rate;
            entity.RateType = model.RateType;
            entity.Nationality = model.Nationality;
            entity.FunctionalAreaId = model.FunctionalAreaId;
            entity.AlternateNumber = model.AlternateNumber;
            entity.ConsultantType = model.ConsultantType;
            entity.ContractType = model.ContractType;
            entity.Gender = model.Gender;
            entity.Profile = model.Profile;
            entity.Experience = new Duration() { Months = model.ExperienceMonths, Years = model.ExperienceYears };
            entity.Location = FindLocation(model.Location);
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

            var IDs = model.Skills.Select(x => x.Id).ToList();
            var existingSkills = _contractorSkillRepository.ById(IDs).ToList();

            //Updated Skills
            for (var i = 0; i < existingSkills.Count; i++)
            {
                var skill = existingSkills[i];
                var mskill = model.Skills.FirstOrDefault(x => x.Id == skill.Id);
                if (mskill == null) continue;
                skill.Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() {Title = mskill.Title, Code = mskill.Code};
                skill.Level = mskill.Level;
                skill.Proficiency = mskill.Proficiency;
                skill.ExperienceInMonths = mskill.ExperienceInMonths;
                _contractorSkillRepository.Update(skill);
            }

            //Created Skills
            var newSkills = model.Skills.Where(x => x.Id == 0);
            foreach (var mskill in newSkills)
            {
                _contractorSkillRepository.Create(new ContractorSkill
                {
                    Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() {Title = mskill.Title, Code = mskill.Code},
                    Level = mskill.Level,
                    Proficiency = mskill.Proficiency,
                    ExperienceInMonths = mskill.ExperienceInMonths,
                    Contractor = entity
                });
            }

            //Deleted Skills
            var deletedSkills = _contractorSkillRepository.All.Where(x => !IDs.Contains(x.Id) && x.ContractorId==entity.Id).ToList();
            for(var i = 0; i < deletedSkills.Count; i++)
            {
                _contractorSkillRepository.Delete(deletedSkills[i]);
            }

            _contractorRepository.Update(entity);
            _contractorRepository.SaveChanges();

            return model;
        }

        public ScheduleViewModel Update(EditScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.Id);
            if(entity == null) throw new Exception("Schedule not found");

            entity.Start = model.Start;
            entity.End = model.End;
            entity.Description = model.Company;
            entity.IsAvailable = model.IsAvailable;

            _scheduleRepository.Update(entity);
            _scheduleRepository.SaveChanges();

            return Schedules.FirstOrDefault(x => x.Id == entity.Id);
        }

        public ContractorSkillEditViewModel Update(ContractorSkillEditViewModel model)
        {
            var contractor = FindContractor(CurrentUserId);

            var entity = _contractorSkillRepository.ById(model.Id);
            if(entity == null || entity.ContractorId != contractor.Id) throw new Exception("Skill not found");

            entity.ExperienceInMonths = model.ExperienceInMonths;
            entity.Level = model.Level;
            entity.Proficiency = model.Proficiency;

            _contractorSkillRepository.Update(entity);
            _scheduleRepository.SaveChanges();

            return Skills.FirstOrDefault(x => x.Id == entity.Id);
        }

        public bool Delete(DeleteScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.Id);
            var contractor = FindContractor(CurrentUserId);
            if(contractor.Id == entity.ContractorId)
            {
                _scheduleRepository.Delete(entity);
                return _scheduleRepository.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(ContractorSkillDeleteViewModel model)
        {
            var entity = _contractorSkillRepository.ById(model.Id);
            var contractor = FindContractor(CurrentUserId);
            if(contractor.Id == entity.ContractorId)
            {
                _contractorSkillRepository.Delete(entity);
                return _contractorSkillRepository.SaveChanges() > 0;
            }
            return false;
        }



        public ScheduleViewModel Create(CreateScheduleViewModel model)
        {
            var contractor = FindContractor(CurrentUserId);
            var entity = new Schedule
            {
                ContractorId = contractor.Id,
                Start = model.Start,
                End = model.End,
                Description = model.Company,
                IsAvailable = model.IsAvailable
            };
            _scheduleRepository.Create(entity);
            _scheduleRepository.SaveChanges();

            return Schedules.FirstOrDefault(x => x.Id == entity.Id);
        }

        public ContractorSkillEditViewModel Create(ContractorSkillCreateViewModel model)
        {
            var contractor = FindContractor(CurrentUserId);
            var skill = _skillRepository.ByCode(model.Code);

            var entity = new ContractorSkill
            {
                ContractorId = contractor.Id,
                Skill = skill,
                ExperienceInMonths = model.ExperienceInMonths,
                Level = model.Level,
                Proficiency = model.Proficiency
            };
            _contractorSkillRepository.Create(entity);
            _contractorSkillRepository.SaveChanges();

            return Skills.FirstOrDefault(x => x.Id == entity.Id);
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
            var contractor = FindContractor(CurrentUserId);

            var jobApplication = new JobApplication
            {
                ContractorId = contractor.Id,
                JobId = model.Id,
            };
            var history = new JobApplicationHistory() { Act = JobActionEnum.Application };
            jobApplication.History.Add(history);
            _jobApplicationRepository.Create(jobApplication);
            _jobApplicationRepository.SaveChanges();
            return Applications(jobApplication.JobId).FirstOrDefault(x => x.Id == jobApplication.Id);
        }


        public ContractorDashboardViewModel GetDashboard(string userId)
        {
            var nextWeek = DateTime.UtcNow.AddDays(7);
            var nextMonth = DateTime.UtcNow.AddMonths(1);
            return new ContractorDashboardViewModel
            {
                Views = _contractorVisitRepository.Mine(userId).Count(),
                Jobs = _jobRepository.MatchingForConctractor(userId).Count(),
                Week = _jobRepository.MatchingForConctractor(userId).Count(x => x.Start < nextWeek),
                Month = _jobRepository.MatchingForConctractor(userId).Count(x => x.Start < nextMonth)
            };
        }
        public void AddView(int id, string userAgent, string ipAddress)
        {
            _contractorVisitRepository.Create(new ContractorVisit
            {
                ContractorId = id,
                IpAddress = ipAddress,
                Browser = userAgent
            });
        }
    }
}