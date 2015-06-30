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
    public class CreateJobViewModel { }
    public class EditJobViewModel { }
    public class JobViewModel { }
    public class DeleteJobViewModel { }
    public class CancelJobViewModel { }
    public class PublishJobViewModel { }
}
