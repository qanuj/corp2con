using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService
    {
        JobApplictionViewModel ApplyToJob(JobApplictionViewModel job);
        bool CancelJob(CancelJobViewModel model);
        ApplyJobApplicationViewModel ApplyToJob(ApplyJobApplicationViewModel job);
        bool CancelJob(CancelJobApplicationViewModel model);
        bool RevokeJobApplication(RevokeJobApplicationViewModel job); 
        //JobVisit

    }
}