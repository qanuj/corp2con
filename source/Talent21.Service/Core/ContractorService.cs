using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using AutoPoco.Configuration;
using e10.Shared.Data.Abstraction;
using e10.Shared.Providers;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class ContractorService : SecuredService, IContractorService
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IContractorVisitRepository _contractorVisitRepository;
        private readonly ICompanyVisitRepository _companyVisitRepository;
        private readonly IJobVisitRepository _jobVisitRepository;

        private readonly IContractorSkillRepository _contractorSkillRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IJobApplicationHistoryRespository _jobApplicationHistoryRespository;
        private readonly IJobRepository _jobRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly INotificationService _notificationService;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAppSiteConfigRepository _appSiteConfigRepository;

        public ContractorService(IContractorRepository contractorRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository,
            ISkillRepository skillRepository,
            IContractorSkillRepository contractorSkillRepository,
            IContractorVisitRepository contractorVisitRepository,
            IJobApplicationHistoryRespository jobApplicationHistoryRespository,
            IJobRepository jobRepository,
            INotificationService notificationService, ICompanyVisitRepository companyVisitRepository, IJobVisitRepository jobVisitRepository,
            IUserProvider userProvider, IAdvertisementRepository advertisementRepository, ITransactionRepository transactionRepository, IAppSiteConfigRepository appSiteConfigRepository) : base(userProvider)
        {
            _jobApplicationHistoryRespository = jobApplicationHistoryRespository;
            _contractorRepository = contractorRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _scheduleRepository = scheduleRepository;
            _skillRepository = skillRepository;
            _contractorSkillRepository = contractorSkillRepository;
            _contractorVisitRepository = contractorVisitRepository;
            _jobRepository = jobRepository;
            _notificationService = notificationService;
            _companyVisitRepository = companyVisitRepository;
            _jobVisitRepository = jobVisitRepository;
            _advertisementRepository = advertisementRepository;
            _transactionRepository = transactionRepository;
            _appSiteConfigRepository = appSiteConfigRepository;
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
                                FunctionalArea = x.FunctionalArea.Title,
                                Industry = x.Industry.Title,
                                IndustryId = x.IndustryId,
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
                                LocationId = x.LocationId,
                                Mobile = x.Mobile,
                                PinCode = x.PinCode,
                                Address = x.Address,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Rss = x.Social.Rss,
                                Twitter = x.Social.Twitter,
                                WebSite = x.Social.WebSite,
                                Yahoo = x.Social.Yahoo,
                                PictureUrl = x.PictureUrl,
                                OwnerId = x.OwnerId,
                                Complete = x.Complete,
                                Rate = x.Rate,
                                RateType = x.RateType,
                                LocationCode = x.Location.Code,
                                Skills = _contractorSkillRepository.All.Where(y => y.ContractorId == x.Id).Select(y => new ContractorSkillViewModel()
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

        public IQueryable<JobBasedJobApplicationHistoryViewModel> ApplicationHistoryByJobIDs(IList<int> IDs)
        {
            return _jobRepository.All.Where(x => IDs.Contains(x.Id)).Select(x => new JobBasedJobApplicationHistoryViewModel
            {
                History = x.Applications.Where(z => z.Contractor.OwnerId == CurrentUserId && IDs.Contains(z.JobId))
                      .SelectMany(z => z.History).Select(y =>
                        new JobApplicationHistoryViewModel()
                        {
                            Act = y.Act,
                            ApplicationId = y.ApplicationId,
                            Created = y.Created,
                            CreateBy = y.CreatedBy
                        }),
                Id = x.Id
            });
        }

        public IQueryable<JobApplicationContractorViewModel> FavoriteJobs()
        {
            return MyApplications().Where(x => x.Actions.Any(y => y.Act == JobActionEnum.Favorite));
        }

        public IQueryable<JobApplicationContractorViewModel> MyApplications()
        {
            return Applications().Where(x => x.Actions.Any());
        }

        public IQueryable<JobApplicationContractorViewModel> Applications(int id = 0)
        {
            return _jobRepository.All.Select(x => new JobApplicationContractorViewModel
            {
                Actions = x.Applications.Where(z => z.Contractor.OwnerId == CurrentUserId && (z.Id == id || id == 0)).SelectMany(z => z.History).Select(y =>
                      new JobApplicationHistoryViewModel() { Act = y.Act, ApplicationId = y.ApplicationId, Created = y.Created, CreateBy = y.CreatedBy }),
                Id = x.Id,
                Company = x.Company.CompanyName,
                IsCancelled = x.IsCancelled,
                Cancelled = x.Cancelled,
                Published = x.Published,
                Skills = x.Skills.Select(y => new JobSkillEditViewModel { Code = y.Skill.Code, Id = y.Id, Title = y.Skill.Title, Level = y.Level }),
                Locations = x.Locations.Select(y => new JobLocationEditViewModel { Code = y.Code, Id = y.Id, Title = y.Title }),
                CompanyId = x.CompanyId,
                Description = x.Description,
                Code = x.Code,
                Title = x.Title,
                End = x.End,
                Rate = x.Rate,
                Start = x.Start,
                IsWorkingFromHome = x.IsWorkingFromHome,
                Positions = x.Positions
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
            return Create(model, null);
        }

        public ContractorEditViewModel Create(ContractorCreateViewModel model,int? companyId)
        {
            var entity = new Contractor
            {
                CompanyId = companyId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                OwnerId = model.OwnerId,
                Email = model.Email,
                PictureUrl = model.PictureUrl,
                About = model.About,
                Rate = model.Rate,
                RateType = model.RateType,
                Nationality = model.Nationality,
                FunctionalAreaId = model.FunctionalAreaId,
                AlternateNumber = model.AlternateNumber,
                ConsultantType = model.ConsultantType,
                ContractType = model.ContractType,
                Gender = model.Gender,
                Profile = model.Profile,
                PinCode = model.PinCode,
                Address = model.Address,
                Experience = new Duration() { Months = model.ExperienceMonths, Years = model.ExperienceYears },
                LocationId = model.LocationId,
                Mobile = model.Mobile,
                Social = new Social
                {
                    Twitter = model.Twitter,
                    Facebook = model.Facebook,
                    Yahoo = model.Yahoo,
                    Google = model.Google,
                    LinkedIn = model.LinkedIn,
                    Rss = model.Rss,
                    WebSite = model.WebSite
                }
            };

            if (model.Skills != null)
            {
                foreach (var mskill in model.Skills)
                {
                    _contractorSkillRepository.Create(new ContractorSkill
                    {
                        Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() { Title = mskill.Title, Code = mskill.Code },
                        Level = mskill.Level,
                        Proficiency = mskill.Proficiency,
                        ExperienceInMonths = mskill.ExperienceInMonths,
                        Contractor = entity
                    });
                }
            }

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

        private void ApplySkills(ContractorEditViewModel model, Contractor entity)
        {
            if (entity.Skills == null) entity.Skills = new List<ContractorSkill>();

            var IDs = entity.Skills.Select(x => x.Id).ToList();
            var existingSkills = _contractorSkillRepository.ById(IDs).ToList();

            //Updated Skills
            for (var i = 0; i < existingSkills.Count; i++)
            {
                var skill = existingSkills[i];
                var mskill = model.Skills.FirstOrDefault(x => x.Id == skill.Id);
                if (mskill == null) continue;
                skill.Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() { Title = mskill.Title, Code = mskill.Code };
                skill.Level = mskill.Level;
                skill.Proficiency = mskill.Proficiency;
                skill.ExperienceInMonths = mskill.ExperienceInMonths;
                _contractorSkillRepository.Update(skill);
            }

            //Created Skills
            var newSkills = model.Skills.Where(x => IDs.All(y => y != x.Id));
            foreach (var mskill in newSkills)
            {
                _contractorSkillRepository.Create(new ContractorSkill
                {
                    Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() { Title = mskill.Title, Code = mskill.Code },
                    Level = mskill.Level,
                    Proficiency = mskill.Proficiency,
                    ExperienceInMonths = mskill.ExperienceInMonths,
                    Contractor = entity
                });
            }

            //Deleted Skills
            var deletedSkills = _contractorSkillRepository.All.Where(x => !IDs.Contains(x.Id) && x.ContractorId == entity.Id).ToList();
            for (var i = 0; i < deletedSkills.Count; i++)
            {
                _contractorSkillRepository.Delete(deletedSkills[i]);
            }

        }

        public ContractorEditViewModel Update(ContractorEditViewModel model)
        {
            var entity = _contractorRepository.ById(model.Id);
            if (entity == null) return null;

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

            entity.PinCode = model.PinCode;
            entity.Address = model.Address;
            entity.Experience = new Duration() { Months = model.ExperienceMonths, Years = model.ExperienceYears };
            entity.LocationId = model.LocationId;
            entity.IndustryId = model.IndustryId;
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

            ApplySkills(model, entity);

            _contractorRepository.Update(entity);
            _contractorRepository.SaveChanges();

            return model;
        }

        public ScheduleViewModel Update(EditScheduleViewModel model)
        {
            var entity = _scheduleRepository.ById(model.Id);
            if (entity == null) throw new Exception("Schedule not found");
            if (!Valid(model,entity.ContractorId,entity.Id)) throw new Exception("Overlapping Schedule");

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
            if (entity == null || entity.ContractorId != contractor.Id) throw new Exception("Skill not found");

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
            if (contractor.Id == entity.ContractorId)
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
            if (contractor.Id == entity.ContractorId)
            {
                _contractorSkillRepository.Delete(entity);
                return _contractorSkillRepository.SaveChanges() > 0;
            }
            return false;
        }

        public bool Valid(CreateScheduleViewModel model,int contractorId,int id=0)
        {
            return !_scheduleRepository.All.Any(x => x.ContractorId==contractorId && (id==0 || x.Id!=id) &&
                ((model.Start >= x.Start && model.Start <= x.End) ||
                (model.End >= x.Start && model.End <= x.End)));
        }

        public ScheduleViewModel Create(CreateScheduleViewModel model)
        {
            var contractor = FindContractor(CurrentUserId);
            return Create(model, contractor.Id);
        }

        public ScheduleViewModel Create(CreateScheduleViewModel model, int contractorId)
        {
            if (!Valid(model, contractorId)) throw new Exception("Overlapping Schedule");
            var entity = new Schedule
            {
                ContractorId = contractorId,
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
            var entity = _jobApplicationRepository.ByJobId(model.Id, CurrentUserId);
            var contractor = FindContractor(CurrentUserId);

            if (entity == null)
            {
                entity = new JobApplication { History = new List<JobApplicationHistory>(), JobId = model.Id, ContractorId = contractor.Id };
                _jobApplicationRepository.Create(entity);
            }

            if (entity.History.All(x => x.Act != model.Act))
            {
                entity.History.Add(new JobApplicationHistory { Act = model.Act, CreatedBy = CurrentUserId, Notes = model.Notes});

                var rowsAffested = _jobApplicationRepository.SaveChanges();

                entity = _jobApplicationRepository.ById(entity.Id);
                _notificationService.ActOnApplication(entity, model.Act);

                return rowsAffested > 0;
            }

            return false;
        }

        public bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act)
        {
            return ActOnApplication(new CompanyActJobApplicationViewModel(model, act));
        }

        public bool Decline(JobDeclineViewModel model)
        {
            return ActOnApplication(new CompanyActJobApplicationViewModel { Act = JobActionEnum.Decline, Notes = model.Reason, Id = model.JobId});
        }

        public bool Promote(PromotionEnum promotion)
        {
            if (promotion == PromotionEnum.None) return false;

            var entity = FindContractor(CurrentUserId);
            if (entity == null) return false;

            if (_advertisementRepository.All.OfType<ContractorAdvertisement>().Any(x => x.Promotion == promotion && x.ContractorId==entity.Id && x.End > DateTime.UtcNow && x.Start < DateTime.UtcNow))
            {
                 throw new Exception("Already running promotion");
            }

            var balance=_transactionRepository.Balance(entity.OwnerId);

            var config = _appSiteConfigRepository.Config();
            var which = config.Contractor.Featured;

            if (promotion == PromotionEnum.Advertise)
            {
                which = config.Contractor.Advertise;
            }
            else if (promotion == PromotionEnum.Global)
            {
                which = config.Contractor.Global;
            }
            else if (promotion == PromotionEnum.Highlight)
            {
                which = config.Contractor.Highlight;
            }

            if (balance < which.Rate) throw new Exception("Not enough balance.");

            var transaction = new AdvertisementTransaction
            {
                Amount = which.Rate * config.Credit.Rate,
                Credit = which.Rate,
                IsSuccess = true, //should come from PayU Money,
                PaymentCapture = "Some Data of Payment Capture",
                UserId = CurrentUserId,
                Advertisement = new ContractorAdvertisement
                {
                    ContractorId = entity.Id,
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow.AddDays(which.Validity),
                    Promotion = promotion,
                    Title = string.Format("Promoted Profile ({1}) as {0}", promotion, entity.Id)
                }
            };

            _advertisementRepository.Create(transaction.Advertisement);
            _transactionRepository.Create(transaction);

            var rowsAffested = _advertisementRepository.SaveChanges();
            return rowsAffested > 0;
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

            jobApplication = _jobApplicationRepository.ById(jobApplication.Id);
            _notificationService.ActOnApplication(jobApplication, JobActionEnum.Application);

            return Applications(jobApplication.JobId).FirstOrDefault(x => x.Id == jobApplication.Id);
        }

        public ContractorViewModel GetFavorite(int id)
        {
            return Contractors.FirstOrDefault(n => n.Id == id);
        }
        public ContractorDashboardViewModel GetDashboard(string userId)
        {
            var nextWeek = DateTime.UtcNow.AddDays(7);
            var nextMonth = DateTime.UtcNow.AddMonths(1);

            var contractor = FindContractor(userId);

            var skill = "Java";
            var location = contractor.Location.Title;

            var skillRow = _contractorSkillRepository.All.Where(x => x.Contractor.OwnerId == userId && x.Level == LevelEnum.Primary)
                .OrderByDescending(x => x.ExperienceInMonths).FirstOrDefault();
            if (skillRow != null) skill = skillRow.Skill.Title;
            
            var aggregateReport = _jobRepository.Durations(location)
                .GroupBy(x => new { x.Skill, x.Location }).Select(x => new
                {
                    Salary = new MinMax { Min = x.Min(y => y.Rate), Max = x.Max(y => y.Rate) },
                    Duration = new MinMax { Min = x.Min(y => y.Duration), Max = x.Max(y => y.Duration) },
                })
                .FirstOrDefault();

            return new ContractorDashboardViewModel
            {
                Aggregate = new ContractorAggregateReport
                {
                    Skill = skill,
                    Location = location,
                    Salary = aggregateReport.Salary,
                    Duration = aggregateReport.Duration,
                },
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

        public bool ActOnApplication(DeleteJobApplicationHistoryViewModel model, JobActionEnum jobActionEnum)
        {
            var entity = _jobApplicationRepository.Contractor(CurrentUserId).FirstOrDefault(x => x.Id == model.Id);
            if (entity == null) return false;

            var favorite = entity.History.FirstOrDefault(x => x.Act == jobActionEnum);
            if (favorite == null) return false;

            _jobApplicationHistoryRespository.Purge(favorite);

            var rowsAffested = _jobApplicationHistoryRespository.SaveChanges();
            return rowsAffested > 0;
        }

        public JobApplicationContractorViewModel JobById(int id)
        {
            return Applications().FirstOrDefault(x => x.Id == id);
        }

        public bool VisitCompany(int id, VisitViewModel model)
        {
            var entity = FindContractor(CurrentUserId);
            var fullName = string.Format("{0} {1}", entity.FirstName, entity.LastName);
            if (!_companyVisitRepository.VisitedEarlier(id, fullName))
            {
                _companyVisitRepository.Create(new CompanyVisit
                {
                    Visitor = fullName,
                    CompanyId = id,
                    Browser = model.Browser,
                    City = model.City,
                    Country = model.Country,
                    IpAddress = model.IpAddress,
                    IsMobile = model.IsMobile,
                    OperatingSystem = model.OperatingSystem,
                    Referer = model.Referer,
                    State = model.State,
                });
                _companyVisitRepository.SaveChanges();
            }
            return true;
        }

        public bool VisitJob(int id, VisitViewModel model)
        {
            var entity = FindContractor(CurrentUserId);
            var fullName = string.Format("{0} {1}", entity.FirstName, entity.LastName);
            if (!_jobVisitRepository.VisitedEarlier(id, fullName))
            {
                _jobVisitRepository.Create(new JobVisit
                {
                    Visitor = fullName,
                    JobId = id,
                    Browser = model.Browser,
                    City = model.City,
                    Country = model.Country,
                    IpAddress = model.IpAddress,
                    IsMobile = model.IsMobile,
                    OperatingSystem = model.OperatingSystem,
                    Referer = model.Referer,
                    State = model.State,
                });
                _jobVisitRepository.SaveChanges();
            }
            return true;
        }
    }
}