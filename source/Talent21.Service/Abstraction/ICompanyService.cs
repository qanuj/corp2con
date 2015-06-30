using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
     public interface ICompanyService : IService,
        IPersonDataService<CompanyEditViewModel, CompanyCreateViewModel, IdModel>
    {
        CompanyViewModel GetProfile(string userId);

        JobViewModel  Create(CreateJobViewModel model);
        JobViewModel Update(EditJobViewModel model);
        bool Delete(DeleteJobViewModel model);
        bool Publish(PublishJobViewModel model);
        bool Cancel(CancelJobViewModel model);

        IQueryable<CompanyViewModel> Companies { get; }

        

    }
}