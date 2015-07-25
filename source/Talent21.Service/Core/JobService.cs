using System;
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
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobService(IJobRepository jobRepository, IJobApplicationRepository jobApplicationRepository, ICompanyRepository companyRepository)
        {
            _jobRepository = jobRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _companyRepository = companyRepository;
        }
        
        public string CurrentUserId { private get; set; }

        protected IQueryable<JobSearchResultViewModel> Jobs
        {
            get
            {
                var query = from job in _jobRepository.All
                            where !job.IsCancelled && job.IsPublished
                            select new JobSearchResultViewModel
                            {
                                LocationCode=job.Location.Code,
                                Code=job.Code,
                                Title=job.Title,
                                Description=job.Description,
                                Location=job.Location.Title,
                                Company=job.Company.CompanyName,
                                Rate=job.Rate,
                                Start=job.Start,
                                End=job.End,
                                Id=job.Id,
                                Skills = job.Skills.Select(y => new DictionaryViewModel() { Code = y.Skill.Code, Title = y.Skill.Title })
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
                query = query.Where(x => x.Location.Contains(model.Location));
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
            return Jobs.Where(x => x.Skills.Any(y => y.Code == skill) && x.LocationCode == location);
        }
    }
}