using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICompanyService : IService
    {
        CreateCompanyViewModel CreateCompany(string name);
        AddProfileViewModel UpdateProfile(AddProfileViewModel profile);
        AddProfileViewModel AddProfile(AddProfileViewModel profile);
        RejectCandidateViewModel RejectCandidate(RejectCandidateViewModel jobApplication);
        ApproveCompanyViewModel ApproveCompany(ApproveCompanyViewModel jobApplication);
        CreateJobApplicationViewModel CreateJob(CreateJobApplicationViewModel jobApplication);
        UpdateJobApplicationViewModel UpdateJob(UpdateJobApplicationViewModel jobApplication);
        CancelJobApplicationViewModel CancelJob(CancelJobApplicationViewModel jobApplication);
        DeleteJobApplicationViewModel DeleteJob(DeleteJobApplicationViewModel jobApplication);
        PublishJobApplicationViewModel PublishJob(PublishJobApplicationViewModel jobApplication);
      //  CompanyVisitJobModel CompanyVisit(CompanyVisitJobModel jobApplication);
        //CompanyVisit
    }
}