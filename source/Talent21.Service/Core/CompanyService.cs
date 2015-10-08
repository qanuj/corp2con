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
using e10.Shared.Extensions;
using e10.Shared.Providers;
using e10.Shared.Respository;

namespace Talent21.Service.Core
{

    public class CompanyService : SecuredService, IViewService, ICompanyService, IFileAccessProvider
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyVisitRepository _companyVisitRepository;
        private readonly IContractorVisitRepository _contractorVisitRepository;
        private readonly IContractorRepository _contractorRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IJobSkillRepository _jobSkillRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IInviteRepository _inviteRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ITransactionRepository _transactionRepository;

        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IContractorSkillRepository _contractorSkillRepository;
        private readonly IContractorFolderRepository _contractorFolderRepository;
        private readonly INotificationService _notificationService;
        private readonly ISharedService _sharedService;
        private readonly IAppSiteConfigRepository _appSiteConfigRepository;

        public CompanyService(ICompanyRepository companyRepository,
            IJobRepository jobRepository,
            ISkillRepository skillRepository,
            IJobSkillRepository jobSkillRepository,
            IJobApplicationRepository jobApplicationRepository,
            IContractorRepository contractorRepository,
            IContractorSkillRepository contractorSkillRepository,
            ICompanyVisitRepository companyVisitRepository,
            IAdvertisementRepository advertisementRepository,
            IScheduleRepository scheduleRepository,
            IContractorFolderRepository contractorFolderRepository,
            INotificationService notificationService,
            IContractorVisitRepository contractorVisitRepository,
            IInviteRepository inviteRepository,
            ISharedService sharedService,
            IUserProvider userProvider, ILocationRepository locationRepository, ITransactionRepository transactionRepository, IAppSiteConfigRepository appSiteConfigRepository) : base(userProvider)
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
            _contractorFolderRepository = contractorFolderRepository;
            _notificationService = notificationService;
            _contractorVisitRepository = contractorVisitRepository;
            _inviteRepository = inviteRepository;
            _sharedService = sharedService;
            _locationRepository = locationRepository;
            _transactionRepository = transactionRepository;
            _appSiteConfigRepository = appSiteConfigRepository;
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
                    LocationId = x.LocationId,
                    Mobile = x.Mobile,
                    Address = x.Address,
                    PinCode = x.PinCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Rss = x.Social.Rss,
                    Twitter = x.Social.Twitter,
                    WebSite = x.Social.WebSite,
                    Yahoo = x.Social.Yahoo,
                    PictureUrl = x.PictureUrl,
                    CompanyName = x.CompanyName,
                    AlternateNumber = x.AlternateNumber,
                    Complete = x.Complete,
                    Industry = new DictionaryViewModel() { Code = x.Industry.Code, Title = x.Industry.Title },
                    IndustryId = x.IndustryId,
                    OrganizationType = x.OrganizationType
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
                Address = model.Address,
                PinCode = model.PinCode,
                LocationId = model.LocationId,
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
            entity.Address = model.Address;
            entity.PinCode = model.PinCode;
            entity.LocationId = model.LocationId;
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
            var company = Companies.FirstOrDefault(x => x.OwnerId == userId);
            if (company == null) return null;
            company.CompanyCode = company.Id.Base10ToString();
            return company;
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
                Duration = new DateRangeData
                {
                    Start = model.Start,
                    End = model.End
                },
                Experience = new IntRangeData
                {
                    Start = model.ExperienceStart,
                    End = model.ExperienceEnd
                },
                Rate = model.Rate,
                IsWorkingFromHome = model.IsWorkingFromHome,
                Positions = model.Positions,
                IsContractExtendable = model.IsContractExtendable,
                IsContractToHire = model.IsContractToHire
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
            var entity = _jobRepository.MineFirst(CurrentUserId, model.Id);
            if (entity == null) throw new Exception("Job Not Found");

            entity.Description = model.Description;
            entity.Code = model.Code;
            entity.Title = model.Title;
            entity.Rate = model.Rate;
            entity.Duration.End = model.End;
            entity.Duration.Start = model.Start;
            entity.Experience.End = model.ExperienceEnd;
            entity.Experience.Start = model.ExperienceStart;
            entity.IsWorkingFromHome = model.IsWorkingFromHome;
            entity.Positions = model.Positions;
            entity.IsContractExtendable = model.IsContractExtendable;
            entity.IsContractToHire = model.IsContractToHire;

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

            var config = _appSiteConfigRepository.Config();
            var balance = _transactionRepository.Balance(CurrentUserId);
            var price = 0 - config.JobPrice.Rate;
            var amount = price * config.Credit.Rate;

            if (balance < price) throw new Exception("Not enough balance.");

            _transactionRepository.Create(new JobTransaction
            {
                JobId = entity.Id,
                Name = entity.Title,
                Credit = price,
                Amount = amount,
                IsSuccess = true,
                Code = Transaction.GenerateTransactionId(),
                UserId = CurrentUserId
            });

            entity.IsPublished = true;
            entity.Published = DateTime.UtcNow;
            entity.Expiry = DateTime.UtcNow.AddDays(config.JobPrice.Validity);

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
                AppicationId = x.Id,
                JobId = x.JobId,
                Folder = x.Folder,
                Id = x.Contractor.Id,
                About = x.Contractor.About,
                Email = x.Contractor.Email,
                Complete = x.Contractor.Complete,
                FunctionalArea = x.Contractor.FunctionalArea.Title,
                Industry = x.Contractor.Industry.Title,
                Experience = x.Contractor.Experience,
                Facebook = x.Contractor.Social.Facebook,
                PinCode = x.Contractor.PinCode,
                Address = x.Contractor.Address,
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
                    Experience = y.Experience,
                    Level = y.Level,
                    Proficiency = y.Proficiency
                })
            });
        }

        public bool ActOnApplication(CompanyActJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if (entity == null) return false;

            entity.History.Add(new JobApplicationHistory() { Act = model.Act, CreatedBy = CurrentUserId });

            var rowsAffested = _jobApplicationRepository.SaveChanges();

            _notificationService.ActOnApplication(entity, model.Act);

            return rowsAffested > 0;
        }

        public bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act)
        {
            return ActOnApplication(new CompanyActJobApplicationViewModel(model, act));
        }

        public bool MoveApplication(FolderMoveViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if (entity == null) return false;

            entity.Folder = model.Folder;

            var rowsAffested = _jobApplicationRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool AddContractorToFolder(FolderMoveViewModel model)
        {
            var company = FindCompany();
            if (company == null) return false;
            _contractorFolderRepository.Create(new ContractorFolder
            {
                Folder = model.Folder,
                CompanyId = company.Id,
                ContractorId = model.Id
            });
            var rowsAffested = _contractorFolderRepository.SaveChanges();
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
                IsContractExtendable = x.IsContractExtendable,
                IsContractToHire = x.IsContractToHire,
                NewApplications = x.Applications.Count(y => !y.IsRevoked),
                Expiry = x.Expiry,
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
                Rate = x.Rate,
                End = x.Duration.End,
                Start = x.Duration.Start,
                ExperienceEnd = x.Experience.End,
                ExperienceStart = x.Experience.Start,
                IsWorkingFromHome = x.IsWorkingFromHome,
                Positions = x.Positions
            });
        }

        public virtual IQueryable<JobViewModel> Jobs
        {
            get { return ToJobViewModel(_jobRepository.Mine(CurrentUserId)); }
        }

        public virtual IQueryable<IdLabel<int>> ActiveJobs
        {
            get
            {
                return ToJobViewModel(_jobRepository.Mine(CurrentUserId))
                  .Where(x => x.IsPublished && !x.IsCancelled && (x.Expiry > DateTime.UtcNow || !x.Expiry.HasValue))
                  .Select(x => new IdLabel<int> { Id = x.Id, Label = x.Title });
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

        public IQueryable<ContractorSearchResultViewModel> Bench(SearchQueryViewModel model)
        {
            var company = FindCompany();
            model.CompanyId = company.Id;
            return Search(model, company).Where(x => x.IsBench);
        }

        public IQueryable<ContractorSearchResultViewModel> Search(SearchQueryViewModel model)
        {
            var company = FindCompany();
            return Search(model, company);
        }

        public ContractorViewModel GetContractorById(int id)
        {
            return Search(new SearchQueryViewModel()).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<ContractorSearchResultViewModel> Search(SearchQueryViewModel model, Company company)
        {
            var skills = string.IsNullOrWhiteSpace(model.Skills) ? new string[] {} : model.Skills.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var hasSkills = skills.Length > 0;

            var query = from x in _contractorRepository.All
                        let availableDay = x.Schedules.Where(y => y.IsAvailable).OrderBy(y => y.Start).Select(y => y.Start).FirstOrDefault()
                        let days = DataFunctions.DiffDays2(DateTime.UtcNow, availableDay)
                        let promotions = x.Advertisements.Where(y => y.End > DateTime.UtcNow && y.Start <= DateTime.UtcNow).Select(z => z.Promotion)
                        let available= days <= 6 ? AvailableEnum.Now : days <= 14 ? AvailableEnum.NextWeek : days <= 30 ? AvailableEnum.NextMonth : AvailableEnum.Later
                        let skill = _contractorSkillRepository.All.Where(y => y.ContractorId == x.Id).Select(y => new ContractorSkillViewModel
                        {
                            Id = y.Id,
                            Code = y.Skill.Code,
                            Title = y.Skill.Title,
                            Experience = y.Experience,
                            Level = y.Level,
                            Proficiency = y.Proficiency
                        })
                        where
                            (model.Folder == null || model.Folder.Trim() == string.Empty || (x.Folders.Any(y => y.Folder == model.Folder) && model.CompanyId == x.CompanyId)) &&
                            (!model.ConsultantTypes.HasValue || x.ConsultantType == model.ConsultantTypes.Value) &&
                            (!model.ContractTypes.HasValue || x.ContractType == model.ContractTypes.Value) &&
                            (model.Locations == null || model.Locations.Trim() == string.Empty || x.Location.Title.Contains(model.Locations)) &&
                            (model.Companies == null || model.Companies.Trim() == string.Empty || x.Company.CompanyName.Contains(model.Companies)) &&
                            (model.Industries == null || model.Industries.Trim() == string.Empty || x.Industry.Title.Contains(model.Industries)) &&
                            (model.Functionals == null || model.Functionals.Trim() == string.Empty || x.FunctionalArea.Title.Contains(model.Functionals)) &&
                            (!model.Available.HasValue || availableDay >= model.Available) &&
                            (!model.Rate.HasValue || x.Rate >= model.Rate) &&
                            (!model.Experience.HasValue || x.Experience >= model.Experience) &&
                            (!hasSkills || skill.Any(y => skills.Any(z => y.Title==z))) &&
                            (model.Keywords == null || model.Keywords.Trim() == string.Empty || x.Company.CompanyName.Contains(model.Keywords) ||
                                                                                                x.Mobile.Contains(model.Keywords) ||
                                                                                                x.Location.Title.Contains(model.Keywords) ||
                                                                                                x.About.Contains(model.Keywords) ||
                                                                                                x.FirstName.Contains(model.Keywords) ||
                                                                                                x.Skills.Any(y => model.Keywords.Contains(y.Skill.Title)) ||
                                                                                                x.LastName.Contains(model.Keywords))
                        select new ContractorSearchResultViewModel
                        {
                            About = x.About,
                            Address = x.Address,
                            AlternateNumber = x.AlternateNumber,
                            Availability = availableDay,
                            Available = available,
                            Company = x.Company != null ? x.Company.CompanyName : "",
                            CompanyId = x.CompanyId,
                            Complete = x.Complete,
                            ConsultantType = x.ConsultantType,
                            ContractType = x.ContractType,
                            Days = days,
                            Email = x.Email,
                            Experience = x.Experience,
                            Facebook = x.Social.Facebook,
                            FirstName = x.FirstName,
                            FunctionalArea = x.FunctionalArea.Title,
                            FunctionalAreaId = x.FunctionalAreaId,
                            Gender = x.Gender,
                            Google = x.Social.Google,
                            Id = x.Id,
                            Industry = x.Industry.Title,
                            IsAdvertised = promotions.Any(y => y == PromotionEnum.Advertise),
                            IsBench = x.CompanyId == company.Id,
                            IsFeatured = promotions.Any(y => y == PromotionEnum.Featured),
                            IsHighlight = promotions.Any(y => y == PromotionEnum.Highlight),
                            IsHome = promotions.Any(y => y == PromotionEnum.Global),
                            LastName = x.LastName,
                            LinkedIn = x.Social.LinkedIn,
                            Location = x.Location.Title,
                            LocationCode = x.Location.Code,
                            Mobile = x.Mobile,
                            Nationality = x.Nationality,
                            OwnerId = x.OwnerId,
                            PictureUrl = x.PictureUrl,
                            PinCode = x.PinCode,
                            ProfileUrl = x.ProfileUrl,
                            Rate = x.Rate,
                            RateType = x.RateType,
                            Rss = x.Social.Rss,
                            Twitter = x.Social.Twitter,
                            WebSite = x.Social.WebSite,
                            Yahoo = x.Social.Yahoo,
                            Skills = skill,
                            Schedules = x.Schedules.Where(y=>y.IsAvailable && !y.IsDeleted).Select(y=>new ScheduleViewModel
                            {
                                Company = y.Description,
                                IsAvailable = y.IsAvailable,
                                Start = y.Start,
                                End=y.End
                            })
                        };
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
                Credits = _sharedService.GetBalance(userId),
                Views = _companyVisitRepository.Mine(userId).Count(),
                Applications = _jobApplicationRepository.Mine(userId).Count(x => x.History.Any(y => y.Act == JobActionEnum.Application) && x.History.All(y => y.Act != JobActionEnum.Rejected) && x.History.All(y => y.Act != JobActionEnum.Revoke)),
                Contractors = _contractorRepository.MatchingCompanyJobs(userId).Count(),
                Jobs = ActiveJobs.Count()
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
            return Search(new SearchQueryViewModel { Skills = skill, Locations = location }).Where(x => x.Skills.Any(y => y.Code == skill) && x.LocationCode == location);
        }

        public IQueryable<AvailableRatedCandidateProfileViewModel> TopRatedAvailableProfiles(string skill, string location)
        {
            return Search(new SearchQueryViewModel { Skills = skill, Locations = location }).Select(x => new AvailableRatedCandidateProfileViewModel
            {
                Id = x.Id,
                Experience = x.Experience,
                Name = x.FirstName + " " + x.LastName,
                Picture = x.PictureUrl,
                Rate = x.Rate,
                Availability = x.Availability,
                Rating = 1
            });
        }

        public bool Promote(PromotionEnum promotion)
        {
            if (promotion == PromotionEnum.None) return false;

            var entity = FindCompany();
            if (entity == null) return false;

            var balance = _transactionRepository.Balance(entity.OwnerId);

            if (_advertisementRepository.All.OfType<CompanyAdvertisement>().Any(x => x.Promotion == promotion && x.CompanyId == entity.Id && x.End > DateTime.UtcNow && x.Start < DateTime.UtcNow))
            {
                throw new Exception("Already running promotion");
            }


            var config = _appSiteConfigRepository.Config();
            var which = config.Company.Featured;

            if (promotion == PromotionEnum.Advertise)
            {
                which = config.Company.Advertise;
            }
            else if (promotion == PromotionEnum.Global)
            {
                which = config.Company.Global;
            }
            else if (promotion == PromotionEnum.Highlight)
            {
                which = config.Company.Highlight;
            }

            if (balance < which.Rate) throw new Exception("Not enough balance.");

            var transaction = new AdvertisementTransaction
            {
                Amount = which.Rate * config.Credit.Rate,
                Credit = which.Rate,
                IsSuccess = true, //should come from PayU Money,
                PaymentCapture = "Some Data of Payment Capture",
                UserId = CurrentUserId,
                Advertisement = new CompanyAdvertisement
                {
                    CompanyId = entity.Id,
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow.AddDays(which.Validity),
                    Promotion = promotion,
                    Title = string.Format("Promoted Company Profile ({1}) as {0}", promotion, entity.Id)
                }
            };

            _advertisementRepository.Create(transaction.Advertisement);
            _transactionRepository.Create(transaction);

            var rowsAffested = _advertisementRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool Promote(PromoteJobViewModel model)
        {
            if (model.Promotion == PromotionEnum.None) return false;

            var entity = _jobRepository.ById(model.Id);
            if (entity == null) return false;

            if (_advertisementRepository.All.OfType<JobAdvertisement>().Any(x => x.Promotion == model.Promotion && x.JobId == entity.Id && x.End > DateTime.UtcNow && x.Start < DateTime.UtcNow))
            {
                throw new Exception("Already running promotion");
            }

            var company = _companyRepository.ById(entity.CompanyId);

            var balance = _transactionRepository.Balance(company.OwnerId);

            var config = _appSiteConfigRepository.Config();
            var which = config.Job.Featured;

            if (model.Promotion == PromotionEnum.Advertise)
            {
                which = config.Job.Advertise;
            }
            else if (model.Promotion == PromotionEnum.Global)
            {
                which = config.Job.Global;
            }
            else if (model.Promotion == PromotionEnum.Highlight)
            {
                which = config.Job.Highlight;
            }

            if (balance < which.Rate) throw new Exception("Not enough balance.");

            var transaction = new AdvertisementTransaction
            {
                Amount = which.Rate * config.Credit.Rate,
                Credit = which.Rate,
                IsSuccess = true, //should come from PayU Money,
                PaymentCapture = "Some Data of Payment Capture",
                UserId = CurrentUserId,
                Advertisement = new JobAdvertisement()
                {
                    JobId = entity.Id,
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow.AddDays(which.Validity),
                    Promotion = model.Promotion,
                    Title = string.Format("Promoted Job ({1}) as {0}", model.Promotion, entity.Id)
                }
            };

            _advertisementRepository.Create(transaction.Advertisement);
            _transactionRepository.Create(transaction);

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

        public IQueryable<CountLabel<int>> JobFolders(int id)
        {
            return Applications().Where(x => x.Job.Id == id).GroupBy(x => x.Folder).Select(x => new CountLabel<int>() { Label = x.Key, Count = x.Count() });
        }

        public IQueryable<CountLabel<int>> ContractorFolders()
        {
            return
                _contractorFolderRepository.Mine(CurrentUserId)
                    .GroupBy(x => x.Folder)
                    .Select(x => new CountLabel<int>() { Label = x.Key, Count = x.Count() });
        }

        public IQueryable<CountLabel<int>> BenchFolders()
        {
            var company = FindCompany();
            return
                _contractorFolderRepository.Mine(CurrentUserId)
                    .Where(x => x.Contractor.CompanyId == company.Id)
                    .GroupBy(x => x.Folder)
                    .Select(x => new CountLabel<int>() { Label = x.Key, Count = x.Count() });
        }

        public bool VisitContractor(int id, VisitViewModel model)
        {
            var company = FindCompany();
            if (!_contractorVisitRepository.VisitedEarlier(id, company.CompanyName))
            {
                _contractorVisitRepository.Create(new ContractorVisit
                {
                    Visitor = company.CompanyName,
                    ContractorId = id,
                    Browser = model.Browser,
                    City = model.City,
                    Country = model.Country,
                    IpAddress = model.IpAddress,
                    IsMobile = model.IsMobile,
                    OperatingSystem = model.OperatingSystem,
                    Referer = model.Referer,
                    State = model.State,
                });
                _contractorVisitRepository.SaveChanges();
            }
            return true;
        }

        public bool InvitePeople(IList<InviteViewModel> model)
        {
            var company = FindCompany();
            var invitees = new List<InviteCodeViewModel>();
            foreach (var invite in model)
            {
                if (!string.IsNullOrWhiteSpace(invite.Email)
                    && !string.IsNullOrWhiteSpace(invite.Name)
                    && invitees.All(x => x.Email != invite.Email)
                    && _inviteRepository.All.OfType<BenchInvite>().All(x => x.Email != invite.Email && x.CompanyId == company.Id))
                {
                    var invitee = new BenchInvite
                    {
                        Code = Guid.NewGuid().ToString().ToLower(),
                        Email = invite.Email,
                        Name = invite.Email,
                        CompanyId = company.Id
                    };
                    _inviteRepository.Create(invitee);
                    invitees.Add(new InviteCodeViewModel(invite) { Code = invitee.Code });
                }
            }
            _inviteRepository.SaveChanges();
            _notificationService.Invite(invitees, company.CompanyName);
            return true;
        }

        public InviteCodeViewModel AcceptInvitation(string code)
        {
            var invite = _inviteRepository.ByCode(code);
            if (invite == null || invite.IsCompleted) return null;
            invite.IsCompleted = true;
            invite.Completed = DateTime.UtcNow;
            _inviteRepository.Update(invite);
            _inviteRepository.SaveChanges();

            var benchInvite = invite as BenchInvite;

            return new InviteCodeViewModel
            {
                Email = invite.Email,
                Name = invite.Name,
                Code = invite.Code,
                CompanyId = benchInvite?.CompanyId
            };
        }

        public bool InviteContractorToJob(JobInviteViewModel model)
        {
            var jobApp = _jobApplicationRepository.ByJobId(model.JobId, model.ContractorId);
            if (jobApp != null && (jobApp.IsRevoked || jobApp.History.Any(x => x.Act == JobActionEnum.Invited ||
                                                                  x.Act == JobActionEnum.Application ||
                                                                  x.Act == JobActionEnum.Decline ||
                                                                  x.Act == JobActionEnum.Rejected ||
                                                                  x.Act == JobActionEnum.Revoke ||
                                                                  x.Act == JobActionEnum.Shortlist)))
            {
                return false;
            }
            if (jobApp == null)
            {
                jobApp = new JobApplication { ContractorId = model.ContractorId, CreatedBy = CurrentUserId, JobId = model.JobId };
                _jobApplicationRepository.Create(jobApp);
                _jobApplicationRepository.SaveChanges();
            }
            return ActOnApplication(new CreateJobApplicationHistoryViewModel { Id = jobApp.Id, Notes = "Invited" }, JobActionEnum.Invited);
        }

        public string BenchOwnerIdById(int benchId)
        {
            var company = FindCompany();
            return Search(new SearchQueryViewModel { CompanyId = company.Id }).Where(x => x.CompanyId == company.Id && x.Id == benchId).Select(x => x.OwnerId).FirstOrDefault();
        }

        public ContractorSearchFilterViewModel ContractorFilters(SearchQueryViewModel model)
        {
            var company = FindCompany();
            var query = Search(model, company);

            var rateValues = new List<dynamic>
            {
                new {Start = 1000, End = 9999},
                new {Start = 10000, End = 19999},
                new {Start = 20000, End = 39999},
                new {Start = 40000, End = 49999},
                new {Start = 50000, End = 79999},
                new {Start = 80000, End = 99999},
                new {Start = 100000, End = 124999},
                new {Start = 125000, End = 149999},
                new {Start = 150000, End = 199999},
                new {Start = 200000, End = 0}
            };

            var startDate = model.Starting ?? DateTime.UtcNow;
            var dates = Enumerable.Range(0, 10).Select(x => new { Start = startDate, End = startDate = startDate.AddDays(x * 7) });

            var starting = from dte in dates
                           from row in query
                           where row.Availability >= dte.Start && row.Availability <= dte.End
                           group dte.Start by dte.Start into grp
                           orderby grp.Key
                           select new FilterLabel<int, DateTime>
                           {
                               Label = grp.Key,
                               Count = grp.Count(),
                               Mode = "date",
                               Selected = model.Starting >= grp.Key
                           };

            var rates = from rate in rateValues
                        from row in query
                        where row.Rate >= rate.Start && (row.Rate <= rate.End || rate.End == 0)
                        group rate.Start by rate.Start into grp
                        orderby grp.Key
                        select new FilterLabel<int, int>
                        {
                            Label = grp.Key,
                            Count = grp.Count(),
                            Mode = "inr",
                            Selected = model.Rate >= grp.Key
                        };


            return new ContractorSearchFilterViewModel
            {
                Companies = query.GroupBy(x => x.Company).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Companies.Contains(x.Key)}).OrderByDescending(x=>x.Count).Take(10),
                Available = starting,
                Rate = rates,
                Experience = new MinMax { Min = query.Min(x => (int?) x.Experience)??0, Max = query.Max(x => (int?)x.Experience)??0 },
                Skills = query.SelectMany(x => x.Skills.Select(y => new { x.Id, Skill = y.Title })).GroupBy(x => x.Skill).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Skills.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
                Industries = query.GroupBy(x => x.Industry).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Industries.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
                Functionals = query.GroupBy(x => x.FunctionalArea).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Functionals.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
                ConsultantTypes = query.GroupBy(x => x.ConsultantType).Select(x => new FilterLabel<int, ContractorTypeEnum> { Count = x.Count(), Label = x.Key, Selected = model.ConsultantTypes==x.Key }).OrderByDescending(x => x.Count).Take(10),
                ContractTypes = query.GroupBy(x => x.ContractType).Select(x => new FilterLabel<int, ContractTypeEnum> { Count = x.Count(), Label = x.Key, Selected = model.ContractTypes == x.Key } ).OrderByDescending(x => x.Count).Take(10),
                Locations = query.GroupBy(x => x.Location).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Locations.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
            };
        }
    }
}