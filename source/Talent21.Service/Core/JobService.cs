using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using e10.Shared.Extensions;
using e10.Shared.Providers;
using e10.Shared.Util;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class JobService : SecuredService, IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IContractorRepository _contractorRepository;

        public JobService(IJobRepository jobRepository,ICompanyRepository companyRepository, IContractorRepository contractorRepository,
            IUserProvider userProvider) : base(userProvider)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _contractorRepository = contractorRepository;
        }

        protected IQueryable<FeaturedCompanyViewModel> Companies
        {
            get
            {
                var query = from company in _companyRepository.All
                            let promotions = company.Advertisements.Where(y => y.End > DateTime.UtcNow && y.Start <= DateTime.UtcNow).Select(z => z.Promotion)
                            select new FeaturedCompanyViewModel
                            {
                                IsFeatured = promotions.Any(y => y == PromotionEnum.Featured),
                                IsHighlight = promotions.Any(y => y == PromotionEnum.Highlight),
                                IsAdvertised = promotions.Any(y => y == PromotionEnum.Advertise),
                                IsHome = promotions.Any(y => y == PromotionEnum.Global),
                                WebSite = company.Social.WebSite,
                                CompanyName = company.CompanyName,
                                PictureUrl = company.PictureUrl,
                                Id = company.Id,
                                Jobs=company.Jobs.Count(x=>!x.IsCancelled && x.IsPublished && (x.Expiry > DateTime.UtcNow || !x.Expiry.HasValue))
                            };
                return query;
            }
        }


        protected IQueryable<FeaturedContractorViewModel> Contractors
        {
            get
            {
                var query = from contractor in _contractorRepository.All
                            let promotions = contractor.Advertisements.Where(y => y.End > DateTime.UtcNow && y.Start <= DateTime.UtcNow).Select(z => z.Promotion)
                            select new FeaturedContractorViewModel
                            {
                                IsFeatured = promotions.Any(y => y == PromotionEnum.Featured),
                                IsHighlight = promotions.Any(y => y == PromotionEnum.Highlight),
                                IsAdvertised = promotions.Any(y => y == PromotionEnum.Advertise),
                                IsHome = promotions.Any(y => y == PromotionEnum.Global),
                                Experience = contractor.Experience,
                                Name = contractor.FirstName +" "+contractor.LastName,
                                PictureUrl = contractor.PictureUrl,
                                Id = contractor.Id,
                                Skills = contractor.Skills.Where(x=>x.Level==LevelEnum.Primary).OrderByDescending(x=>x.Experience).Take(5).Select(x => x.Skill.Title)
                            };
                return query;
            }
        }

        protected IQueryable<JobSearchResultViewModel> Search()
        {
            return Search(new SearchQueryViewModel());
        } 
        public IQueryable<JobSearchResultViewModel> Search(SearchQueryViewModel model)
        {
            var skills = string.IsNullOrWhiteSpace(model.Skills) ? new string[] { } : model.Skills.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var hasSkills = skills.Length > 0;

            var locations = string.IsNullOrWhiteSpace(model.Locations) ? new string[] { } : model.Locations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var hasLocations = locations.Length > 0;

            var query = from job in _jobRepository.All
                        where !job.IsCancelled && job.IsPublished && (job.Expiry > DateTime.UtcNow || !job.Expiry.HasValue)
                        let promotions = job.Advertisements.Where(y => y.End > DateTime.UtcNow && y.Start <= DateTime.UtcNow).Select(x => x.Promotion)
                        where
                            (!hasSkills || job.Skills.Any(y => skills.Any(z => y.Skill.Title == z))) &&
                            (!hasLocations || job.Locations.Any(y => locations.Any(z => y.Title == z))) &&
                            (!model.ContractExtendable.HasValue || job.IsContractExtendable == model.ContractExtendable) &&
                            (!model.ContractToHire.HasValue || job.IsContractToHire == model.ContractToHire) &&
                            (!model.CompanyId.HasValue || job.CompanyId == model.CompanyId) &&
                            (model.RateStart <= 0 || job.Rate >= model.RateStart) &&
                            (model.RateEnd <= 0 || job.Rate <= model.RateEnd) &&
                            (!model.Starting.HasValue || job.Duration.Start >= model.Starting) &&
                            (model.ExperienceStart <= 0 || job.Experience.Start >= model.ExperienceStart) &&
                            (model.ExperienceEnd <= 0 || job.Experience.Start <= model.ExperienceEnd) &&
                            (model.Companies == null || model.Companies.Trim() == string.Empty || job.Company.CompanyName.Contains(model.Companies)) &&
                            (model.Keywords == null || model.Keywords.Trim() == string.Empty || job.Company.CompanyName.Contains(model.Keywords) ||
                                                                                                job.Title.Contains(model.Keywords) ||
                                                                                                job.Description.Contains(model.Keywords))
                        select new JobSearchResultViewModel
                        {
                            IsContractExtendable=job.IsContractExtendable,
                            IsContractToHire=job.IsContractToHire,
                            Industry = job.Company.Industry.Title,
                            CompanyId = job.CompanyId,
                            PictureUrl = job.Company.PictureUrl,
                            FirstName = job.Company.FirstName,
                            LastName = job.Company.LastName,
                            Mobile = job.Company.Mobile,
                            Address = job.Company.Address,
                            PinCode = job.Company.PinCode,
                            WebSite = job.Company.Social.WebSite,
                            About = job.Company.About,
                            Email = job.Company.Email,
                            Code = job.Code,
                            Title = job.Title,
                            Description = job.Description,
                            Company = job.Company.CompanyName,
                            Rate = job.Rate,
                            IsWorkingFromHome = job.IsWorkingFromHome,
                            Positions = job.Positions,
                            Start = job.Duration.Start,
                            End = job.Duration.End,
                            ExperienceStart = job.Experience.Start,
                            ExperienceEnd = job.Experience.End,
                            Id = job.Id,
                            IsFeatured = promotions.Any(y => y == PromotionEnum.Featured),
                            IsHighlight = promotions.Any(y => y == PromotionEnum.Highlight),
                            IsAdvertised = promotions.Any(y => y == PromotionEnum.Advertise),
                            IsHome = promotions.Any(y => y == PromotionEnum.Global),
                            Skills = job.Skills.Select(y => new DictionaryViewModel() { Code = y.Skill.Code, Title = y.Skill.Title }),
                            Locations = job.Locations.Select(y => new DictionaryViewModel() { Code = y.Code, Title = y.Title })
                        };

            return query;
        }

        public JobSearchResultViewModel ById(int id)
        {
            return Search().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<PictureViewModel> TopEmployers(string skill, string location)
        {
            var query = from company in _companyRepository.All
                join job in _jobRepository.All on company.Id equals job.CompanyId
                select new PictureViewModel
                {
                    Id = company.Id,
                    Name = company.CompanyName,
                    Picture = company.PictureUrl
                };
            return query;
        }

        public IQueryable<JobSearchResultViewModel> TopJobs(string skill, string location)
        {
            return Search(new SearchQueryViewModel { Skills = skill, Locations = location});
        }

        public IList<JobSearchResultViewModel> LatestJobs(int count)
        {
            return Search().OrderByDescending(x => x.Id).Take(count).ToList();
        }

        public JobSearchResultViewModel GetFeaturedJob()
        {
            return Search().OrderByDescending(x=>x.Id).FirstOrDefault(x => x.IsHome);
        }

        public IList<FeaturedCompanyViewModel> GetFeaturedCompanies(int count)
        {
            return Companies.Where(x => x.IsHome).OrderByDescending(x => x.Id).Take(count).ToList();
        }

        public StatsViewModel GetStats()
        {
            return new StatsViewModel
            {
                Jobs = _jobRepository.All.Count(x=>!x.IsCancelled && x.IsPublished),
                Companies = _companyRepository.All.Count(),
                Contractors = _contractorRepository.All.Count()
            };
        }

        public FeaturedContractorViewModel GetFeaturedContractor()
        {
            return Contractors.OrderByDescending(x => x.Id).FirstOrDefault(x => x.IsHome);
        }

        public IQueryable<JobPublicViewModel> Applications(int id = 0)
        {
            return _jobRepository.All.Select(x => new JobPublicViewModel
            {
                PictureUrl = x.Company.PictureUrl,
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
                Rate = x.Rate,
                End = x.Duration.End,
                Start = x.Duration.Start,
                ExperienceEnd = x.Experience.End,
                ExperienceStart = x.Experience.Start,
                IsWorkingFromHome = x.IsWorkingFromHome,
                Positions = x.Positions
            });
        }

        public JobPublicViewModel JobById(int id)
        {
            var job = Applications().FirstOrDefault(x => x.Id == id);
            if (job == null) return null;
            job.JobCode = job.Id.Base10ToString();
            return job;
        }

        protected IQueryable<CompanyPublicViewModel> PublicCompany
        {
            get
            {
                return _companyRepository.All.Select(x => new CompanyPublicViewModel
                {
                    Id = x.Id,
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
                    IndustryId = x.IndustryId,
                    OrganizationType = x.OrganizationType
                });
            }
        }
        public Job FullById(int id)
        {
            return _jobRepository.All.Include(x => x.Skills.Select(y => y.Skill))
                    .Include(x => x.Locations)
                    .Include(x => x.Company.Location)
                    .FirstOrDefault(x => x.Id == id);
        }

        public JobSearchFilterViewModel JobFilters(SearchQueryViewModel model)
        {
            var startDate = model.Starting ?? DateTime.UtcNow;
            var dates = Enumerable.Range(0, 10).Select(x => new { Start= startDate, End = startDate = startDate.AddDays(x * 7) });
            var query = Search(model);

            var starting=from dte in dates
                         from row in query
                         where row.Start >= dte.Start && row.Start <= dte.End
                         group dte.Start by dte.Start into grp
                         orderby grp.Key 
                         select new FilterLabel<int,DateTime>
                         {
                             Label = grp.Key,
                             Count = grp.Count(),
                             Mode = "date",
                             Selected = model.Starting >= grp.Key
                         };

            return new JobSearchFilterViewModel
            {
                Starting = starting,
                Rate = query.GroupBy(x => x.Rate / 10000).Select(x => new MinMaxLabel<int> { Label = x.Key, Min = x.Min(y => y.Rate), Max = x.Max(y => y.Rate) }).Take(10),
                Companies = query.GroupBy(x => x.Company).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Companies.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
                Experience = new MinMax { Min = query.Min(x => (int?)x.ExperienceStart) ?? 0, Max = query.Max(x => (int?)x.ExperienceEnd) ?? 0 },
                Skills = query.SelectMany(x => x.Skills.Select(y => new { x.Id, Skill = y.Title })).GroupBy(x => x.Skill).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Skills.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
                Industries = query.GroupBy(x => x.Industry).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Industries.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
                ContractExtendable = query.GroupBy(x => x.IsContractExtendable).Select(x => new FilterLabel<int, bool?> { Count = x.Count(), Label = x.Key, Selected = model.ContractExtendable == x.Key }).OrderByDescending(x => x.Count).Take(10),
                ContractToHire = query.GroupBy(x => x.IsContractToHire).Select(x => new FilterLabel<int, bool?> { Count = x.Count(), Label = x.Key, Selected = model.ContractToHire == x.Key }).OrderByDescending(x => x.Count).Take(10),
                Locations = query.SelectMany(x => x.Locations.Select(y => new { x.Id, Location = y.Title })).GroupBy(x => x.Location).Select(x => new FilterLabel<int> { Count = x.Count(), Label = x.Key, Selected = model.Locations.Contains(x.Key) }).OrderByDescending(x => x.Count).Take(10),
            };
        }

        public CompanyPublicViewModel CompanyById(int id)
        {
            var company = PublicCompany.FirstOrDefault(x => x.Id == id);
            if (company == null) return null;
            company.CompanyCode = company.Id.Base10ToString();
            return company;
        }
    }
}