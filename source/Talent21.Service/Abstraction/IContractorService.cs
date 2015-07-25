using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IContractorService : IService,ISecuredService,ISharedService,
        IPersonDataService<ContractorEditViewModel, ContractorCreateViewModel,IdModel>
    {
        ContractorViewModel GetProfile(string userId);
        IQueryable<ContractorViewModel> Contractors { get; }
        IQueryable<ScheduleViewModel> Schedules { get; }
        IQueryable<ContractorSkillViewModel> Skills { get; }
        IQueryable<JobApplicationContractorViewModel> Applications(int id = 0);

        ScheduleViewModel Create(CreateScheduleViewModel model);
        ScheduleViewModel Update(EditScheduleViewModel model);
        bool Delete(DeleteScheduleViewModel model);

        JobApplicationViewModel Apply(JobApplicationCreateViewModel model);

        bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum jobActionEnum);

        ContractorSkillEditViewModel Create(ContractorSkillCreateViewModel model);
        ContractorSkillEditViewModel Update(ContractorSkillEditViewModel model);
        bool Delete(ContractorSkillDeleteViewModel model);


        ContractorViewModel GetProfile(int id);
        ContractorDashboardViewModel GetDashboard(string userId);
    }
}