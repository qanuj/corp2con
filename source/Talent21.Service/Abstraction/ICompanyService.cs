using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICompanyService : IService
    {
        CreateCompanyViewModel CreateCompany(string name);
        UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile);
        AddProfileViewModel AddProfile(AddProfileViewModel profile);
        bool RejectCandidate(RejectCandidateViewModel jobApplication);
        bool ApproveCompany(ApproveCompanyViewModel jobApplication);
        CreateJobApplicationViewModel CreateJob(CreateJobApplicationViewModel jobApplication);
        UpdateJobApplicationViewModel UpdateJob(UpdateJobApplicationViewModel jobApplication);
       // bool CancelJob(CancelJobApplicationViewModel jobApplication);
        DeleteJobApplicationViewModel DeleteJob(DeleteJobApplicationViewModel jobApplication);
        PublishJobApplicationViewModel PublishJob(PublishJobApplicationViewModel jobApplication);
      //  CompanyVisitJobModel CompanyVisit(CompanyVisitJobModel jobApplication);
        //CompanyVisit
    }
}