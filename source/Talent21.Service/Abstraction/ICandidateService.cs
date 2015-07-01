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
        string CurrentUserId { set; }
        IQueryable<ScheduleViewModel> Schedules { get; }
    }
}