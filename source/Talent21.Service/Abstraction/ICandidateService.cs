using System.Linq;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICandidateService : IService
    {
        JobApplicationViewModel CreateCandidateAndApplyToJob(CreateCandidateAndApplyToJobViewModel model);
        UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile);
        CreateCandidateViewModel CreateCandidate(CreateCandidateViewModel profile);
        //ScheduleViewModel AddSchedule(ScheduleCreateViewModel schedule);
        CreateScheduleViewModel CreateSchedule(CreateScheduleViewModel schedule);
        bool DeleteProfile(DeleteProfileViewModel profile);
        AddScheduleViewModel AddSchedule(AddScheduleViewModel schedule);
        UpdateScheduleViewModel UpdateSchedule(UpdateScheduleViewModel schedule);
        DeleteScheduleViewModel DeleteSchedule(DeleteScheduleViewModel schedule);
        ScheduleViewModel ViewSchedule(ScheduleViewModel schedule);
        //CandidateJob

        CandidatePublicProfileViewModel GetProfile(int id);

        IQueryable<CandidatePublicProfileViewModel> GetProfileQuery();

        ScheduleViewModel GetSchedule(int id);
        IQueryable<ScheduleViewModel> GetSchedules();
    }
}