using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService
    {
        JobApplicationViewModel ApplyToJob(JobApplicationViewModel job);
        bool CancelJob(CancelJobApplicationViewModel jobApplication);
        ApplyJobApplicationViewModel ApplyToJob(ApplyJobApplicationViewModel job);
        bool RevokeJobApplication(RevokeJobApplicationViewModel job); 
        //JobVisit

    }
}