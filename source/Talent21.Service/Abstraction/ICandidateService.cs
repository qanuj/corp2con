using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICandidateService : IService, IPersonDataService<ContractorEditViewModel, ContractorCreateViewModel,IdModel>
    {
        ContractorViewModel GetProfile(string userId);
        IQueryable<ContractorViewModel> Contractors { get; }
        string CurrentUserId { set; }
        IQueryable<ScheduleViewModel> Schedules { get; }

        ScheduleViewModel Create(CreateScheduleViewModel model);
        ScheduleViewModel Update(EditScheduleViewModel model);
        bool Delete(DeleteScheduleViewModel model);

        JobApplicationViewModel Apply(JobApplicationCreateViewModel model);

        bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum jobActionEnum);
    }
}