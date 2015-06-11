using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICandidateService : IService
    {
        CandidateProfileViewModel UpdateProfile(CandidateProfileViewModel profile);
        ScheduleViewModel AddSchedule(ScheduleCreateViewModel schedule);
        ScheduleCreateViewModel CreateSchedule(ScheduleCreateViewModel schedule1);
        CandidateDeleteProfileModel DeleteProfile(CandidateDeleteProfileModel profile1);
        CandidateJobViewModel UpdateJob(CandidateJobViewModel job);
        CandidateAddJobViewModel AddJob(CandidateAddJobViewModel job1);

    }
}