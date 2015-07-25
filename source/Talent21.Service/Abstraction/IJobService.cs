using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService, ISecuredService
    {
        IQueryable<JobSearchResultViewModel> Search(SearchQueryViewModel model);
        JobSearchResultViewModel ById(int id);
        IQueryable<PictureViewModel> TopEmployers(string skill, string location);
        IQueryable<JobSearchResultViewModel> TopJobs(string skill, string location);
    }
}