using System;
using System.Collections.Generic;
using System.Linq;
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
        
        protected IQueryable<JobSearchResultViewModel> Jobs
        {
            get
            {
                var query = from job in _jobRepository.All
                            where !job.IsCancelled && job.IsPublished && (job.Expiry > DateTime.UtcNow || !job.Expiry.HasValue)
                            let promotions= job.Advertisements.Where(y => y.End > DateTime.UtcNow && y.Start <= DateTime.UtcNow).Select(x=>x.Promotion)
                            select new JobSearchResultViewModel
                            {
                                CompanyId=job.CompanyId,
                                PictureUrl=job.Company.PictureUrl,
                                FirstName = job.Company.FirstName,
                                LastName = job.Company.LastName,
                                Mobile = job.Company.Mobile,
                                Address = job.Company.Address,
                                PinCode = job.Company.PinCode,
                                WebSite = job.Company.Social.WebSite,
                                About = job.Company.About,
                                Email = job.Company.Email,
                                Code=job.Code,
                                Title=job.Title,
                                Description=job.Description,
                                Company=job.Company.CompanyName,
                                Rate=job.Rate,
                                Start=job.Start,
                                IsWorkingFromHome=job.IsWorkingFromHome,
                                Positions=job.Positions,
                                End=job.End,
                                Id=job.Id,
                                IsFeatured = promotions.Any(y => y == PromotionEnum.Feartured),
                                IsHighlight = promotions.Any(y => y == PromotionEnum.Highlight),
                                IsAdvertised = promotions.Any(y => y == PromotionEnum.Advertise),
                                IsHome = promotions.Any(y => y == PromotionEnum.Global),
                                Skills = job.Skills.Select(y => new DictionaryViewModel() { Code = y.Skill.Code, Title = y.Skill.Title }),
                                Locations = job.Locations.Select(y => new DictionaryViewModel() { Code = y.Code, Title = y.Title })
                            };
                return query;
            }
        }

        protected IQueryable<FeaturedCompanyViewModel> Companies
        {
            get
            {
                var query = from company in _companyRepository.All
                            let promotions = company.Advertisements.Where(y => y.End > DateTime.UtcNow && y.Start <= DateTime.UtcNow).Select(z => z.Promotion)
                            select new FeaturedCompanyViewModel
                            {
                                IsFeatured = promotions.Any(y => y == PromotionEnum.Feartured),
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
                                IsFeatured = promotions.Any(y => y == PromotionEnum.Feartured),
                                IsHighlight = promotions.Any(y => y == PromotionEnum.Highlight),
                                IsAdvertised = promotions.Any(y => y == PromotionEnum.Advertise),
                                IsHome = promotions.Any(y => y == PromotionEnum.Global),
                                Experience = contractor.Experience.Years+"."+contractor.Experience.Months,
                                Name = contractor.FirstName +" "+contractor.LastName,
                                PictureUrl = contractor.PictureUrl,
                                Id = contractor.Id,
                                Skills = contractor.Skills.Where(x=>x.Level==LevelEnum.Primary).OrderByDescending(x=>x.ExperienceInMonths).Take(5).Select(x => x.Skill.Title)
                            };
                return query;
            }
        }

        public IQueryable<JobSearchResultViewModel> Search(SearchQueryViewModel model)
        {
            var query = Jobs;
            //Rules of searching.
            if(!string.IsNullOrWhiteSpace(model.Location))
            {
                query = query.Where(x => x.Locations.Any(y=>y.Title==model.Location));
            }
            if (!string.IsNullOrWhiteSpace(model.Keywords))
            {
                query = query.Where(x =>
                    x.Title.Contains(model.Keywords) ||
                    x.Description.Contains(model.Keywords) ||
                    x.Company.Contains(model.Keywords) ||
                    x.About.Contains(model.Keywords)
                );
            }
            if (!string.IsNullOrWhiteSpace(model.Skills))
            {
                var skills = model.Skills.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(x => x.Skills.Any(y => skills.Any(z => y.Title.Contains(z))));
            }
            if (model.CompanyId > 0)
            {
                query=query.Where(x => x.CompanyId == model.CompanyId);
            }

            return query;
        }

        public JobSearchResultViewModel ById(int id)
        {
            return Jobs.FirstOrDefault(x => x.Id == id);
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
            return Jobs.Where(x => x.Skills.Any(y => y.Code == skill) && x.Locations.Any(y=>y.Code==location));
        }

        public IList<JobSearchResultViewModel> LatestJobs(int count)
        {
            return Jobs.OrderByDescending(x => x.Id).Take(count).ToList();
        }

        public JobSearchResultViewModel GetFeaturedJob()
        {
            return Jobs.OrderByDescending(x=>x.Id).FirstOrDefault(x => x.IsHome);
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
    }
}