using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
     public interface ICompanyService : IService,
        IPersonDataService<CompanyEditViewModel, CompanyCreateViewModel, IdModel>
     {
        string CurrentUserId {set;}
        CompanyViewModel GetProfile(string userId);

        JobViewModels  Create(CreateJobViewModel model);
        JobViewModels Update(EditJobViewModel model);
        bool Delete(DeleteJobViewModel model);
        bool Publish(PublishJobViewModel model);
        bool Cancel(CancelJobViewModel model);

        IQueryable<CompanyViewModel> Companies { get; }
        IQueryable<JobViewModels> Jobs { get; }
    }
}