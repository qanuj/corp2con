using System;
using System.Linq;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public abstract class SharedService : ISharedService, ISecuredService
    {
        private readonly IJobRepository _jobRepository;

        protected SharedService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public string CurrentUserId { set; protected get; }

        public IQueryable<JobViewModels> Jobs
        {
            get
            {
                return _jobRepository.All.Where(x => x.Company.OwnerId == CurrentUserId).Select(x => new JobViewModels
                {
                    Id = x.Id,
                    Applied = x.Applications.Count,
                    Company = x.Company.CompanyName,
                    IsCancelled = x.IsCancelled,
                    Cancelled = x.Cancelled,
                    IsPublished = x.IsPublished,
                    Published = x.Published,
                    Location = x.Location.Title,
                    Skills = x.Skills.Select(y => new SkillDictionaryViewModel { Code = y.Code, Id = y.Id, Title = y.Title }),
                    CompanyId = x.CompanyId,
                    Description = x.Description,
                    Code = x.Code,
                    Title = x.Title,
                    End = x.End,
                    LocationId = x.LocationId,
                    Rate = x.Rate,
                    Start = x.Start
                });
            }
        }
    }
}