using System;
using System.Collections.Generic;
using System.Linq;
using e10.Shared.Util;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IContractorRepository _contractorRepository;

        public JobService(IJobRepository jobRepository,ICompanyRepository companyRepository, IContractorRepository contractorRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _contractorRepository = contractorRepository;
        }
        
        public string CurrentUserId { private get; set; }

        protected IQueryable<JobSearchResultViewModel> Jobs
        {
            get
            {
                //TODO:color coded jobs based on dates.
                
                var query = from job in _jobRepository.All
                            where !job.IsCancelled && job.IsPublished
                            select new JobSearchResultViewModel
                            {
                                PictureUrl=job.Company.PictureUrl,
                                FirstName = job.Company.FirstName,
                                LastName = job.Company.LastName,
                                Mobile = job.Company.Mobile,
                                WebSite = job.Company.Social.WebSite,
                                About = job.Company.About,
                                Email = job.Company.Email,
                                Code=job.Code,
                                Title=job.Title,
                                Description=job.Description,
                                Company=job.Company.CompanyName,
                                Rate=job.Rate,
                                Start=job.Start,
                                End=job.End,
                                Id=job.Id,
                                Promotion = job.Advertisements.Where(x => x.Start <= DateTime.UtcNow && x.End > DateTime.UtcNow).Select(x=>x.Promotion).FirstOrDefault(),
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
                //TODO:color coded jobs based on dates.

                var query = from company in _companyRepository.All
                            select new FeaturedCompanyViewModel
                            {
                                WebSite = company.Social.WebSite,
                                CompanyName = company.CompanyName,
                                PictureUrl = company.PictureUrl,
                                Id = company.Id,
                                Jobs=company.Jobs.Count(x=>!x.IsCancelled && x.IsPublished),
                                Promotion = company.Advertisements.Where(x => x.Start <= DateTime.UtcNow && x.End > DateTime.UtcNow).Select(x => x.Promotion).FirstOrDefault(),
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
            if(!string.IsNullOrWhiteSpace(model.Skills))
            {
                //TODO: AND OR LOGIC
                var skills = model.Skills.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(x => x.Skills.Any(y => skills.Any(z => y.Title.Contains(z))));
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
            return Jobs.OrderByDescending(x=>x.Id).FirstOrDefault(x => x.Promotion==PromotionEnum.Global);
        }

        public IList<FeaturedCompanyViewModel> GetFeaturedCompanies(int count)
        {
            return Companies.Where(x => x.Promotion == PromotionEnum.Global).OrderByDescending(x => x.Id).Take(count).ToList();
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
    }
}