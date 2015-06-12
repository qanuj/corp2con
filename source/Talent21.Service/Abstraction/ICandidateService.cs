using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICandidateService : IService
    {
        CandidateProfileViewModel UpdateProfile(CandidateProfileViewModel profile);
        CandidateViewModel CreateCandidate(CandidateCreateViewModel profile);
        //ScheduleViewModel AddSchedule(ScheduleCreateViewModel schedule);
        ScheduleCreateViewModel CreateSchedule(ScheduleCreateViewModel schedule);
        bool DeleteProfile(CandidateDeleteProfileModel profile);
        CandidateAddScheduleModel AddSchedule(CandidateAddScheduleModel schedule);
        CandidateUpdateScheduleModel UpdateSchedule(CandidateUpdateScheduleModel schedule);
        CandidateDeleteScheduleModel DeleteSchedule(CandidateDeleteScheduleModel schedule);
        CandidateViewScheduleModel ViewSchedule(CandidateViewScheduleModel schedule);
        //CandidateJob
    }
}