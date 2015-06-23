using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface IJobService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        JobApplicationViewModel ApplyToJob(JobApplicationViewModel job);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        bool CancelJob(CancelJobApplicationViewModel jobApplication);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        ApplyJobApplicationViewModel ApplyToJob(ApplyJobApplicationViewModel job);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        bool RevokeJobApplication(RevokeJobApplicationViewModel job); 
        

    }
}