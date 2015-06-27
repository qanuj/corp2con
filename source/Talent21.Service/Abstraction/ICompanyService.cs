using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICompanyService : IService, IPersonDataService<CompanyEditViewModel, CompanyCreateViewModel, IdModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        CreateCompanyViewModel CreateCompany(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        AddProfileViewModel AddProfile(AddProfileViewModel profile);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        bool RejectCandidate(RejectCandidateViewModel jobApplication);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        bool ApproveCompany(ApproveCompanyViewModel jobApplication);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        CreateJobApplicationViewModel CreateJob(CreateJobApplicationViewModel jobApplication);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        UpdateJobApplicationViewModel UpdateJob(UpdateJobApplicationViewModel jobApplication);

       // bool CancelJob(CancelJobApplicationViewModel jobApplication);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        DeleteJobApplicationViewModel DeleteJob(DeleteJobApplicationViewModel jobApplication);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        PublishJobApplicationViewModel PublishJob(PublishJobApplicationViewModel jobApplication);

      //  CompanyVisitJobModel CompanyVisit(CompanyVisitJobModel jobApplication);
        //CompanyVisit

        CreateCompanyViewModel CreateCompany(CreateCompanyViewModel model);
    }
}