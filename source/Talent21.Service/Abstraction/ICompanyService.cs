using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISharedService
    {
        IQueryable<JobViewModels> Jobs { get; }
    }


     public interface ICompanyService : IService,
         ISecuredService,
         ISharedService,
         IPersonDataService<CompanyEditViewModel, CompanyCreateViewModel, IdModel>
     {
        CompanyViewModel GetProfile(string userId);

        JobViewModels  Create(CreateJobViewModel model);
        JobViewModels Update(EditJobViewModel model);
        bool Delete(DeleteJobViewModel model);
        bool Publish(PublishJobViewModel model);
        bool Cancel(CancelJobViewModel model);

        IQueryable<CompanyViewModel> Companies { get; }

        IQueryable<JobApplicationViewModel> Applications(int id);
        bool ActOnApplication(CompanyActJobApplicationViewModel act);
        bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act);
        bool MoveApplication(MoveJobApplicationViewModel model);

        JobViewModels ById(int id);
     }
}