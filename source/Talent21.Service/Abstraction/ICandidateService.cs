using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICandidateService : IService
    {
        JobApplictionViewModel CreateCandidateAndApplyToJob(CreateCandidateAndApplyToJobViewModel model);
        CandidateProfileViewModel UpdateProfile(CandidateProfileViewModel profile);
        CandidateViewModel CreateCandidate(CandidateCreateViewModel profile);
        //ScheduleViewModel AddSchedule(ScheduleCreateViewModel schedule);
        ScheduleCreateViewModel CreateSchedule(ScheduleCreateViewModel schedule);
        bool DeleteProfile(CandidateDeleteProfileModel profile);
        CreateScheduleViewModel AddSchedule(CreateScheduleViewModel schedule);
        CandidateUpdateScheduleModel UpdateSchedule(CandidateUpdateScheduleModel schedule);
        DeleteScheduleViewModel DeleteSchedule(DeleteScheduleViewModel schedule);
        CandidateViewScheduleModel ViewSchedule(CandidateViewScheduleModel schedule);
        //CandidateJob
    }
}