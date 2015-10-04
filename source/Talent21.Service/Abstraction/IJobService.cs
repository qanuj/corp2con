using System.Collections.Generic;
using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService, ISecuredService
    {
        IQueryable<JobSearchResultViewModel> Search(SearchQueryViewModel model);
        JobSearchResultViewModel ById(int id);
        IQueryable<PictureViewModel> TopEmployers(string skill, string location);
        IQueryable<JobSearchResultViewModel> TopJobs(string skill, string location);
        IList<JobSearchResultViewModel> LatestJobs(int count);
        JobSearchResultViewModel GetFeaturedJob();
        IList<FeaturedCompanyViewModel> GetFeaturedCompanies(int count);
        StatsViewModel GetStats();
        FeaturedContractorViewModel GetFeaturedContractor();
        JobPublicViewModel JobById(int id);
        CompanyPublicViewModel CompanyById(int id);
        Job FullById(int id);
        JobSearchFilterViewModel JobFilters(SearchQueryViewModel model);
    }
}