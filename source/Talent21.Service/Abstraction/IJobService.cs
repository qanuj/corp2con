namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService
    {
        CandidateJobViewModel ApplyToJob(CandidateJobViewModel job);
        CandidateAddJobViewModel CancelJob(CandidateAddJobViewModel job);
        CandidateRevokeJobModel RevokeJob(CandidateRevokeJobModel job); 
    }
}