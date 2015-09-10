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
        private readonly SellingOptions _sellingOptions;

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
            IUserProvider userProvider, ILocationRepository locationRepository, ITransactionRepository transactionRepository, SellingOptions sellingOptions) :base(userProvider)
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
            _sellingOptions = sellingOptions;
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
                    Address =x.Address,
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
                Address = model.Address,
                PinCode = model.PinCode,
                Profile = model.Profile,
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
            entity.Profile = model.Profile;
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
                Start = model.Start,
                IsWorkingFromHome = model.IsWorkingFromHome,
                Positions = model.Positions
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
            entity.End = model.End;
            entity.Rate = model.Rate;
            entity.Start = model.Start;
            entity.IsWorkingFromHome = model.IsWorkingFromHome;
            entity.Positions = model.Positions;

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

            var balance = _transactionRepository.Balance(CurrentUserId);
            var price = 0 - _sellingOptions.RequirementCredit;
            var amount = price * _sellingOptions.CreditPrice;

            if (balance < price) throw new Exception("Not enough balance.");
            _transactionRepository.Create(new JobTransaction
            {
                Name = entity.Title,
                Credit = price,
                Amount = amount,
                IsSuccess = true,
                Code = Transaction.GenerateTransactionId(),
                UserId = CurrentUserId
            });

            entity.IsPublished = true;
            entity.Published = DateTime.UtcNow;
            entity.Expiry = DateTime.UtcNow.AddDays(_sellingOptions.Validity);

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
                Folder = x.Folder,
                Contractor = new ContractorViewModel
                {
                    Id = x.Contractor.Id,
                    About = x.Contractor.About,
                    Email = x.Contractor.Email,
                    Complete = x.Contractor.Complete,
                    FunctionalArea = x.Contractor.FunctionalArea.Title,
                    Industry = x.Contractor.Industry.Title,
                    ExperienceMonths = x.Contractor.Experience.Months,
                    ExperienceYears = x.Contractor.Experience.Years,
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
                NewApplications=x.Applications.Count(y=>!y.IsRevoked),
                Expiry=x.Expiry,
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
                Start = x.Start,
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
            get { return ToJobViewModel(_jobRepository.Mine(CurrentUserId))
                    .Where(x=>x.IsPublished && !x.IsCancelled && (x.Expiry>DateTime.UtcNow || !x.Expiry.HasValue))
                    .Select(x=>new IdLabel<int> { Id = x.Id,Label = x.Title}); }
        }

        public IQueryable<ContractorSearchResultViewModel> Contractors
        {
            get
            {
                var query = from x in _contractorRepository.All
                            let availableDay = x.Schedules.Where(y => y.IsAvailable).OrderBy(y => y.Start).Select(y => y.Start).FirstOrDefault()
                            let days = DataFunctions.DiffDays2(DateTime.UtcNow, availableDay)
                            select new ContractorSearchResultViewModel
                            {
                                Company = x.Company!=null ? x.Company.CompanyName: "",
                                CompanyId = x.CompanyId,
                                Folders = x.Folders.Select(z => new CompanyFolderViewModel { Folder = z.Folder, CompanyId = z.CompanyId }),
                                Id = x.Id,
                                About = x.About,
                                Email = x.Email,
                                RateType = x.RateType,
                                Nationality = x.Nationality,
                                AlternateNumber = x.AlternateNumber,
                                ConsultantType = x.ConsultantType,
                                ContractType = x.ContractType,
                                Gender = x.Gender,
                                Profile = x.Profile,
                                FunctionalArea = x.FunctionalArea.Title,
                                Industry = x.Industry.Title,
                                FunctionalAreaId = x.FunctionalAreaId,
                                ExperienceMonths = x.Experience.Months,
                                ExperienceYears = x.Experience.Years,
                                PinCode = x.PinCode,
                                Address = x.Address,
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
                                Available = days <= 6 ? AvailableEnum.Now : days <= 14 ? AvailableEnum.NextWeek : days <= 30 ? AvailableEnum.NextMonth : AvailableEnum.Later,
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

        public IQueryable<ContractorSearchResultViewModel> Bench(SearchQueryViewModel model)
        {
            var company = FindCompany();
            return Search(model, company).Where(x => x.CompanyId == company.Id);
        }

        public IQueryable<ContractorSearchResultViewModel> Search(SearchQueryViewModel model)
        {
            var company = FindCompany();
            return Search(model, company);
        }

        public IQueryable<ContractorSearchResultViewModel> Search(SearchQueryViewModel model,Company company)
        {
            var query = Contractors;
            //Rules of searching.
            if (!string.IsNullOrWhiteSpace(model.Location))
            {
                query = query.Where(x => x.Location.Contains(model.Location));
            }
            if (model.IndustryId > 0)
            {
                query = query.Where(x => x.IndustryId == model.IndustryId);
            }
            if (model.RateType.HasValue)
            {
                query = query.Where(x => x.RateType == model.RateType.Value);
            }

            if (model.RateStart > 0)
            {
                query = query.Where(x => x.Rate > model.RateStart);
            }
            if (model.RateEnd > 0)
            {
                query = query.Where(x => x.Rate < model.RateEnd);
            }
            if (!string.IsNullOrWhiteSpace(model.Skills))
            {
                var skills = model.Skills.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(x => x.Skills.Any(y => skills.Any(z => y.Title.Contains(z))));
            }
            if (model.xFrom > 0)
            {
                query = query.Where(x => (x.ExperienceYears * 12 + x.ExperienceMonths) > model.xFrom);
            }
            if (model.xTo > 0)
            {
                query = query.Where(x => (x.ExperienceYears * 12 + x.ExperienceMonths) < model.xTo);
            }

            if (!string.IsNullOrWhiteSpace(model.Keywords))
            {
                if (company != null)
                {
                    query = query.Where(x =>
                        x.Company.Contains(model.Keywords) ||
                        x.Mobile.Contains(model.Keywords) ||
                        x.Profile.Contains(model.Keywords) ||
                        x.Location.Contains(model.Keywords) ||
                        x.About.Contains(model.Keywords) ||
                        x.FirstName.Contains(model.Keywords) ||
                        x.LastName.Contains(model.Keywords) 
                    );
                }

            }
            if (!string.IsNullOrWhiteSpace(model.Folder))
            {
                if (company != null)
                {
                    query = query.Where(x => x.Folders.Any(y => y.Folder == model.Folder && y.CompanyId == company.Id));
                }

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

            var transaction = new AdvertisementTransaction
            {
                Amount = ((int)model.Promotion) * 10,
                Credit = ((int)model.Promotion) * 100,
                IsSuccess = true, //should come from PayU Money,
                PaymentCapture = "Some Data of Payment Capture",
                UserId = CurrentUserId,
                Advertisement = new JobAdvertisement()
                {
                    JobId = entity.Id,
                    Start = DateTime.UtcNow,
                    End = DateTime.UtcNow.AddDays(30),
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
                    .Where(x=>x.Contractor.CompanyId == company.Id)
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
                    && _inviteRepository.All.OfType<BenchInvite>().All(x => x.Email != invite.Email && x.CompanyId==company.Id))
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
                CompanyId = benchInvite == null ? (int?)null : benchInvite.CompanyId
            };
        }

        public bool InviteContractorToJob(JobInviteViewModel model)
        {
            var jobApp = _jobApplicationRepository.ByJobId(model.JobId,model.ContractorId);
            if (jobApp != null && (jobApp.IsRevoked || jobApp.History.Any(x => x.Act == JobActionEnum.Invited ||
                                                                  x.Act == JobActionEnum.Application ||
                                                                  x.Act == JobActionEnum.Decline ||
                                                                  x.Act == JobActionEnum.Rejected ||
                                                                  x.Act == JobActionEnum.Revoke ||
                                                                  x.Act == JobActionEnum.Shortlist)))
            {
                return false;
            }
            if (jobApp == null){
                jobApp=new JobApplication { ContractorId = model.ContractorId, CreatedBy = CurrentUserId, JobId = model.JobId};
                _jobApplicationRepository.Create(jobApp);
                _jobApplicationRepository.SaveChanges();
            }
            return ActOnApplication(new CreateJobApplicationHistoryViewModel { Id = jobApp.Id, Notes = "Invited" },JobActionEnum.Invited);
        }
    }
}