using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICandidateService : IService, IPersonDataService<ContractorEditViewModel, ContractorCreateViewModel,IdModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JobApplicationViewModel CreateCandidateAndApplyToJob(CreateCandidateAndApplyToJobViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        CreateCandidateViewModel CreateCandidate(CreateCandidateViewModel profile);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        CreateScheduleViewModel CreateSchedule(CreateScheduleViewModel schedule);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        bool DeleteProfile(DeleteProfileViewModel profile);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        AddScheduleViewModel AddSchedule(AddScheduleViewModel schedule);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        UpdateScheduleViewModel UpdateSchedule(UpdateScheduleViewModel schedule);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        DeleteScheduleViewModel DeleteSchedule(DeleteScheduleViewModel schedule);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        ScheduleViewModel ViewSchedule(ScheduleViewModel schedule);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CandidatePublicProfileViewModel GetProfile(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<CandidatePublicProfileViewModel> GetProfileQuery();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ScheduleViewModel GetSchedule(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<ScheduleViewModel> GetSchedules();

        IQueryable<ContractorViewModel> Contractors { get; }
    }
}