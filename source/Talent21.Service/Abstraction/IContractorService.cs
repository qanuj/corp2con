using System.Collections.Generic;
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
        IQueryable<JobApplicationContractorViewModel> MyApplications();
        IQueryable<JobBasedJobApplicationHistoryViewModel> ApplicationHistoryByJobIDs(IList<int> jobIds);
        IQueryable<JobApplicationContractorViewModel> FavoriteJobs();

        ScheduleViewModel Create(CreateScheduleViewModel model);
        ScheduleViewModel Update(EditScheduleViewModel model);
        bool Delete(DeleteScheduleViewModel model);

        JobApplicationViewModel Apply(JobApplicationCreateViewModel model);

        bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum jobActionEnum);

        ContractorSkillEditViewModel Create(ContractorSkillCreateViewModel model);
        ScheduleViewModel Create(CreateScheduleViewModel model, int contractorId);
        ContractorSkillEditViewModel Update(ContractorSkillEditViewModel model);
        bool Delete(ContractorSkillDeleteViewModel model);


        ContractorViewModel GetFavorite(int id);
        ContractorDashboardViewModel GetDashboard(string userId);

        bool ActOnApplication(DeleteJobApplicationHistoryViewModel model, JobActionEnum jobActionEnum);

        JobApplicationContractorViewModel JobById(int id);

        bool VisitCompany(int id, VisitViewModel model);
        bool VisitJob(int id, VisitViewModel model);
    }
}