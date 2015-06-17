using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService
    {
        CandidateJobViewModel ApplyToJob(CandidateJobViewModel job);
        bool CancelJob(CancelJobViewModel model);
        bool RevokeJobApplication(RevokeJobApplicationViewModel job); 
        //JobVisit

    }
}