using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICandidateService : IService
    {
        ProfileViewModel UpdateProfile(ProfileViewModel profile);
        ScheduleViewModel AddSchedule(ScheduleCreateViewModel schedule);
    }
}