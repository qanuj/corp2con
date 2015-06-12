using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICompanyService : IService
    {
        CompanyViewModel CreateCompany(string name);
        CompanyProfileViewModel UpdateProfile(CompanyProfileViewModel profile);
        CompanyProfileAddModel AddProfile(CompanyProfileAddModel profile);
        CandidateRejectModel RejectCandidate(CandidateRejectModel jobApplication);
        CompanyApproveModel ApproveCompany(CompanyApproveModel jobApplication);
        CompanyCreateJobModel CreateJob(CompanyCreateJobModel jobApplication);
        CompanyUpdateJobModel UpdateJob(CompanyUpdateJobModel jobApplication);
        CompanyCancelJobModel CancelJob(CompanyCancelJobModel jobApplication);
        CompanyDeleteJobModel DeleteJob(CompanyDeleteJobModel jobApplication);
        CompanyPublishJobModel PublishJob(CompanyPublishJobModel jobApplication);
        //CompanyVisit
    }
}