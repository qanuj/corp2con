using System.Linq;
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
        IQueryable<ScheduleViewModel> Schedules(int contractorId);
    }
}