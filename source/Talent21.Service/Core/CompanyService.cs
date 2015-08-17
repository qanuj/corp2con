using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Linq;
using e10.Shared;

namespace Talent21.Service.Core
{

    public class CompanyService : SharedService, ICompanyService, IFileAccessProvider
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyVisitRepository _companyVisitRepository;
        private readonly IContractorRepository _contractorRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IJobSkillRepository _jobSkillRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IScheduleRepository _scheduleRepository;

        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IContractorSkillRepository _contractorSkillRepository;

        public CompanyService(ICompanyRepository companyRepository,
            IJobRepository jobRepository,
            ISkillRepository skillRepository,
            IJobSkillRepository jobSkillRepository,
            IJobApplicationRepository jobApplicationRepository,
            ILocationRepository locationRepository,
            IContractorRepository contractorRepository,
            IContractorSkillRepository contractorSkillRepository,
            ICompanyVisitRepository companyVisitRepository,
            ITransactionRepository transactionRepository,
            IAdvertisementRepository advertisementRepository, IScheduleRepository scheduleRepository)
            : base(locationRepository, transactionRepository)
        {
            _jobSkillRepository = jobSkillRepository;
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _skillRepository = skillRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _contractorRepository = contractorRepository;
            _contractorSkillRepository = contractorSkillRepository;
            _companyVisitRepository = companyVisitRepository;
            _advertisementRepository = advertisementRepository;
            _scheduleRepository = scheduleRepository;
        }

        public IQueryable<CompanyViewModel> Companies
        {
            get
            {
                return _companyRepository.All.Select(x => new CompanyViewModel
                {
                    Id = x.Id,
                    OwnerId = x.OwnerId,
                    About = x.About,
                    Email = x.Email,
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
                    CompanyName = x.CompanyName,
                    AlternateNumber = x.AlternateNumber,
                    Industry = new DictionaryViewModel() { Code = x.Industry.Code, Title = x.Industry.Title },
                    OrganizationType = x.OrganizationType,
                    Profile = x.Profile
                });
            }
        }

        public bool Delete(IdModel model)
        {
            _companyRepository.Delete(model.Id);
            var rowsAffested = _companyRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public CompanyEditViewModel Create(CompanyCreateViewModel model)
        {
            var entity = new Company
            {
                OwnerId = model.OwnerId,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PictureUrl = model.PictureUrl,
                About = model.About,
                CompanyName = model.CompanyName,
                OrganizationType = model.OrganizationType,
                AlternateNumber = model.AlternateNumber,
                Profile = model.Profile,
                Location = FindLocation(model.Location),
                Mobile = model.Mobile,
                IndustryId = model.IndustryId,
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

            _companyRepository.Create(entity);
            _companyRepository.SaveChanges();
            return new CompanyEditViewModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                Email = entity.Email
            };
        }

        public CompanyEditViewModel Update(CompanyEditViewModel model)
        {
            var entity = _companyRepository.ById(model.Id);
            if (entity == null) return null;

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.PictureUrl = model.PictureUrl;
            entity.Email = model.Email;
            entity.About = model.About;
            entity.CompanyName = model.CompanyName;
            entity.OrganizationType = model.OrganizationType;
            entity.AlternateNumber = model.AlternateNumber;
            entity.Profile = model.Profile;
            entity.Location = FindLocation(model.Location);
            entity.Mobile = model.Mobile;
            entity.IndustryId = model.IndustryId;
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

            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();

            return model;
        }

        public CompanyViewModel GetProfile(string userId)
        {
            return Companies.FirstOrDefault(x => x.OwnerId == userId);
        }

        public CompanyViewModel GetProfile(int id)
        {
            return Companies.FirstOrDefault(x => x.Id == id);
        }

        private void ApplySkills(CreateJobViewModel model, Job entity)
        {

            var IDs = entity.Skills.Select(x => x.Id).ToList();
            var existingSkills = _jobSkillRepository.ById(IDs).ToList();

            //Updated Skills
            for (var i = 0; i < existingSkills.Count; i++)
            {
                var skill = existingSkills[i];
                var mskill = model.Skills.FirstOrDefault(x => x.Id == skill.Id);
                if (mskill == null) continue;
                skill.Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() { Title = mskill.Title, Code = mskill.Code };
                skill.Level = mskill.Level;
                _jobSkillRepository.Update(skill);
            }

            //Created Skills
            var newSkills = model.Skills.Where(x => IDs.All(y => y != x.Id));
            foreach (var mskill in newSkills)
            {
                _jobSkillRepository.Create(new JobSkill
                {
                    Skill = _skillRepository.ByTitle(mskill.Title) ?? new Skill() { Title = mskill.Title, Code = mskill.Code },
                    Level = mskill.Level,
                    Job = entity
                });
            }

            //Deleted Skills
            var deletedSkills = _jobSkillRepository.All.Where(x => !IDs.Contains(x.Id) && x.JobId == entity.Id).ToList();
            for (var i = 0; i < deletedSkills.Count; i++)
            {
                _jobSkillRepository.Delete(deletedSkills[i]);
            }
        }

        public JobViewModel Create(CreateJobViewModel model)
        {
            var company = FindCompany();
            return Create(model, company.Id);

        }
        public JobViewModel Create(CreateJobViewModel model, int companyId)
        {
            var entity = new Job
            {
                CompanyId = companyId,
                Description = model.Description,
                Code = model.Code,
                Title = model.Title,
                End = model.End,
                Rate = model.Rate,
                Start = model.Start
            };

            ApplySkills(model, entity);
            ApplyLocations(model, entity);

            _jobRepository.Create(entity);
            _jobRepository.SaveChanges();

            return Jobs.FirstOrDefault(x => x.Id == entity.Id);

        }

        private void ApplyLocations(CreateJobViewModel model, Job entity)
        {
            if (entity.Locations == null) entity.Locations = new List<Location>();

            var IDs = model.Locations.Select(x => x.Id).ToList();
            var xIDs = entity.Locations.Select(x => x.Id).ToList();

            //Created Skills
            var newLocs = model.Locations.Where(x => xIDs.All(y => y != x.Id));
            foreach (var mloc in newLocs)
            {
                var loc = _locationRepository.ByTitle(mloc.Title) ?? new Location() { Title = mloc.Title, Code = mloc.Code };
                entity.Locations.Add(loc);
            }

            //Deleted Skills
            var deletedLocations = _locationRepository.All.Where(x => !IDs.Contains(x.Id) && xIDs.Contains(x.Id)).ToList();
            for (var i = 0; i < deletedLocations.Count; i++)
            {
                entity.Locations.Remove(deletedLocations[i]);
            }
        }


        private Company FindCompany()
        {
            var company = ByOwner(CurrentUserId);
            if (company == null) throw new Exception("Company Not found");
            return company;
        }

        private Company ByOwner(string userId)
        {
            return _companyRepository.All.FirstOrDefault(x => x.OwnerId == userId);
        }

        public JobViewModel Update(EditJobViewModel model)
        {
            var entity = _jobRepository.MineFirst(CurrentUserId,model.Id);
            if (entity == null) throw new Exception("Job Not Found");

            entity.Description = model.Description;
            entity.Code = model.Code;
            entity.Title = model.Title;
            entity.End = model.End;
            entity.Rate = model.Rate;
            entity.Start = model.Start;

            ApplySkills(model, entity);
            ApplyLocations(model, entity);

            _jobRepository.Update(entity);
            _jobRepository.SaveChanges();

            return Jobs.FirstOrDefault(x => x.Id == entity.Id);
        }

        public bool Delete(DeleteJobViewModel model)
        {
            _jobRepository.Delete(model.Id);
            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool Publish(PublishJobViewModel model)
        {
            var entity = _jobRepository.ById(model.Id);
            if (entity == null) return false;

            entity.IsPublished = true;
            entity.Published = DateTime.UtcNow;

            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool Cancel(CancelJobViewModel model)
        {
            var entity = _jobRepository.ById(model.Id);
            if (entity == null) return false;

            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;

            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
        }


        public IQueryable<JobApplicationCompanyViewModel> Applications(int id = 0)
        {
            return _jobApplicationRepository.All.Where(x => x.JobId == id || (id == 0 && x.Job.Company.OwnerId == CurrentUserId)).Select(x => new JobApplicationCompanyViewModel
            {
                Job = new DictionaryEditViewModel { Id = x.JobId, Code = x.Job.Code, Title = x.Job.Title },
                Actions = x.History.Select(y => new JobApplicationHistoryViewModel() { Act = y.Act, Created = y.Created, CreateBy = y.CreatedBy }),
                Id = x.Id,
                Contractor = new ContractorViewModel
                {
                    Id = x.Contractor.Id,
                    About = x.Contractor.About,
                    Email = x.Contractor.Email,
                    ExperienceMonths = x.Contractor.Experience.Months,
                    ExperienceYears = x.Contractor.Experience.Years,
                    Facebook = x.Contractor.Social.Facebook,
                    Google = x.Contractor.Social.Google,
                    LinkedIn = x.Contractor.Social.LinkedIn,
                    Location = x.Contractor.Location.Title,
                    Mobile = x.Contractor.Mobile,
                    FirstName = x.Contractor.FirstName,
                    LastName = x.Contractor.LastName,
                    Rss = x.Contractor.Social.Rss,
                    Twitter = x.Contractor.Social.Twitter,
                    WebSite = x.Contractor.Social.WebSite,
                    Yahoo = x.Contractor.Social.Yahoo,
                    PictureUrl = x.Contractor.PictureUrl,
                    OwnerId = x.Contractor.OwnerId,
                    Rate = x.Contractor.Rate,
                    Skills = x.Contractor.Skills.Select(y => new ContractorSkillViewModel()
                    {
                        Code = y.Skill.Code,
                        Title = y.Skill.Title,
                        ExperienceInMonths = y.ExperienceInMonths,
                        Level = y.Level,
                        Proficiency = y.Proficiency
                    })
                }
            });
        }

        public bool ActOnApplication(CompanyActJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if (entity == null) return false;

            entity.History.Add(new JobApplicationHistory() { Act = model.Act, CreatedBy = CurrentUserId });

            var rowsAffested = _jobApplicationRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act)
        {
            return ActOnApplication(new CompanyActJobApplicationViewModel(model, act));
        }

        public bool MoveApplication(MoveJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if (entity == null) return false;

            entity.Folder = model.Folder;

            var rowsAffested = _jobApplicationRepository.SaveChanges();
            return rowsAffested > 0;
        }


        public JobViewModel ById(int id)
        {
            return Jobs.FirstOrDefault(x => x.Id == id);
        }

        private static IQueryable<JobViewModel> ToJobViewModel(IQueryable<Job> jobs)
        {
            return jobs.Select(x => new JobViewModel
            {
                Id = x.Id,
                Applied = x.Applications.Count,
                Company = x.Company.CompanyName,
                IsCancelled = x.IsCancelled,
                Cancelled = x.Cancelled,
                IsPublished = x.IsPublished,
                Published = x.Published,
                Skills = x.Skills.Select(y => new JobSkillEditViewModel { Code = y.Skill.Code, Level = y.Level, Id = y.Id, Title = y.Skill.Title }),
                Locations = x.Locations.Select(y => new JobLocationEditViewModel { Code = y.Code, Id = y.Id, Title = y.Title }),
                CompanyId = x.CompanyId,
                Description = x.Description,
                Code = x.Code,
                Title = x.Title,
                End = x.End,
                Rate = x.Rate,
                Start = x.Start
            });
        }

        public virtual IQueryable<JobViewModel> Jobs
        {
            get { return ToJobViewModel(_jobRepository.Mine(CurrentUserId)); }
        }

        public IQueryable<ContractorSearchResultViewModel> Contractors
        {
            get
            {
                var query = from x in _contractorRepository.All
                            let availableDay = x.Schedules.Where(y => y.IsAvailable).OrderBy(y => y.Start).Select(y => y.Start).FirstOrDefault()
                            let days = DataFunctions.DiffDays2(DateTime.UtcNow,availableDay)
                            select new ContractorSearchResultViewModel
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
                                LocationCode = x.Location.Code,
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
                                Availability = availableDay,
                                Days = days,
                                Available= days<=6 ? AvailableEnum.Now : days <= 14 ? AvailableEnum.NextWeek : days <= 30 ? AvailableEnum.NextMonth: AvailableEnum.Later,
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

        public async Task<FileAccessInfo> ByUrlAsync(string userId, string filepath)
        {
            var jobApplication = await _jobApplicationRepository.MineAsync(userId, filepath);
            if (jobApplication == null) return null;
            return new FileAccessInfo
            {
                Id = jobApplication.Job.Code,
                Location = jobApplication.Contractor.Location.Title,
                Name = string.Format("{0}_{1}", jobApplication.Contractor.FirstName, jobApplication.Contractor.LastName)
            };
        }


        public IQueryable<ContractorSearchResultViewModel> Search(SearchQueryViewModel model)
        {
            var query = Contractors;
            //Rules of searching.
            if (!string.IsNullOrWhiteSpace(model.Location))
            {
                query = query.Where(x => x.Location.Contains(model.Location));
            }
            if (!string.IsNullOrWhiteSpace(model.Skills))
            {
                var skills = model.Skills.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(x => x.Skills.Any(y => skills.Any(z => y.Title.Contains(z))));
            }
            return query;
        }


        public CompanyDashboardViewModel GetDashboard(string userId)
        {
            var skill = "Java";
            var location = "Hydrabad";

            var skillRow = _jobSkillRepository.All.Where(x => x.Job.Company.OwnerId == userId).GroupBy(x => x.Skill.Title).Select(x => new { Skill = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).FirstOrDefault();
            if (skillRow != null) skill = skillRow.Skill;

            var locationRow = _jobRepository.All.Where(x => x.Company.OwnerId == userId && x.Skills.Any(y => y.Skill.Title == skill))
                .SelectMany(x => x.Locations.Select(y => new { Location = y.Title }))
                .GroupBy(x => x.Location).Select(x => new { Location = x.Key, count = x.Count() })
                .OrderByDescending(x => x.count).FirstOrDefault();
            if (locationRow != null) location = locationRow.Location;

            var aggregateReport = _contractorRepository.All
                .SelectMany(x => x.Skills.Where(y => y.Level == LevelEnum.Primary).Select(y => new { Skill = y.Skill.Title, Location = x.Location.Title, x.Rate, Available = x.Schedules.Where(z => z.IsAvailable).OrderBy(z => z.Start).Select(z => z.Start).FirstOrDefault() }))
                .GroupBy(x => new { x.Skill, x.Location }).Select(x => new
                {
                    Salary = new MinMax { Min = x.Min(y => y.Rate), Max = x.Max(y => y.Rate) },
                    Duration = new MinMax<DateTime> { Min = x.Min(y => y.Available), Max = x.Max(y => y.Available) },
                })
                .FirstOrDefault();

            return new CompanyDashboardViewModel
            {
                Aggregate = new CompanyAggregateReport
                {
                    Skill = skill,
                    Location = location,
                    Salary = aggregateReport.Salary,
                    Duration = aggregateReport.Duration,
                },
                Views = _companyVisitRepository.Mine(userId).Count(),
                Applications = _jobApplicationRepository.Mine(userId).Count(x => x.History.Any(y => y.Act == JobActionEnum.Application) && x.History.All(y => y.Act != JobActionEnum.Rejected) && x.History.All(y => y.Act != JobActionEnum.Revoke)),
                Contractors = _contractorRepository.MatchingCompanyJobs(userId).Count(),
                Jobs = _jobRepository.Mine(userId).Count(x => x.IsPublished)
            };
        }
        public void AddView(int id, string userAgent, string ipAddress)
        {
            _companyVisitRepository.Create(new CompanyVisit()
            {
                CompanyId = id,
                IpAddress = ipAddress,
                Browser = userAgent
            });
        }

        public IQueryable<ContractorSearchResultViewModel> LatestProfiles(string skill, string location)
        {
            return Contractors.Where(x => x.Skills.Any(y => y.Code == skill) && x.LocationCode == location);
        }

        public IQueryable<AvailableRatedCandidateProfileViewModel> TopRatedAvailableProfiles(string skill, string location)
        {
            return Contractors.Where(x => x.Skills.Any(y => y.Code == skill) && x.LocationCode == location).Select(x => new AvailableRatedCandidateProfileViewModel
            {
                Id = x.Id,
                ExperienceInMonths = x.ExperienceMonths,
                ExperienceInYears = x.ExperienceYears,
                Name = x.FirstName + " " + x.LastName,
                Picture = x.PictureUrl,
                Rate = x.Rate,
                Availability = x.Availability,
                Rating = 1
            });
        }

        public bool Promote(PromoteJobViewModel model)
        {
            var entity = _jobRepository.ById(model.Id);
            if (entity == null) return false;

            _advertisementRepository.Create(new JobAdvertisement()
            {
                JobId = entity.Id,
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow.AddDays(30),
                Promotion = model.Promotion,
                Transaction = new AdvertisementTransaction
                {
                    Amount = ((int)model.Promotion) * 10,
                    Credit = ((int)model.Promotion) * 100,
                    IsSuccess = true, //should come from PayU Money,
                    PaymentCapture = "Some Data of Payment Capture",
                    UserId = CurrentUserId
                },
                Title = string.Format("Promoted Job ({1}) as {0}", model.Promotion, entity.Id)
            });

            var rowsAffested = _advertisementRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public IQueryable<ScheduleViewModel> Schedules(int id)
        {
            return _scheduleRepository.All.Where(x => x.ContractorId == id && x.IsAvailable).Select(x => new ScheduleViewModel
            {
                Id = x.Id,
                Start = x.Start,
                End = x.End,
                Company = "Available",
                IsAvailable = x.IsAvailable
            });
        }

        public JobApplicationCompanyViewModel Application(int id)
        {
            return Applications().FirstOrDefault(x => x.Id == id);
        }
    }
}