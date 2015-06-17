using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService
    {
        JobApplictionViewModel ApplyToJob(JobApplictionViewModel job);
        bool CancelJob(CancelJobViewModel model);
        bool RevokeJobApplication(RevokeJobApplicationViewModel job); 
        //JobVisit

    }
}