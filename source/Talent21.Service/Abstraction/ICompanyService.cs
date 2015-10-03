using System.Collections.Generic;
using System.Linq;
using Talent21.Data.Core;
using Talent21.Service.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ICompanyService : IService,
         ISecuredService,
         IPersonDataService<CompanyEditViewModel, CompanyCreateViewModel, IdModel>
     {
        CompanyViewModel GetProfile(string userId);
        CompanyViewModel GetProfile(int id);

        JobViewModel  Create(CreateJobViewModel model);
        JobViewModel Create(CreateJobViewModel model, int companyId);

        JobViewModel Update(EditJobViewModel model);
        bool Delete(DeleteJobViewModel model);
        bool Publish(PublishJobViewModel model);
        bool Cancel(CancelJobViewModel model);
        IQueryable<CompanyViewModel> Companies { get; }
        IQueryable<JobViewModel> Jobs { get; }
        IQueryable<IdLabel<int>> ActiveJobs { get; }

        IQueryable<JobApplicationCompanyViewModel> Applications(int id);
        bool ActOnApplication(CompanyActJobApplicationViewModel act);
        bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act);
        bool MoveApplication(FolderMoveViewModel model);
        bool AddContractorToFolder(FolderMoveViewModel model);

        JobViewModel ById(int id);

        IQueryable<ContractorSearchResultViewModel> Search(SearchQueryViewModel model);
        IQueryable<ContractorSearchResultViewModel> Bench(SearchQueryViewModel model);
        CompanyDashboardViewModel GetDashboard(string userId);
        IQueryable<ContractorSearchResultViewModel> LatestProfiles(string skill, string location);
        IQueryable<AvailableRatedCandidateProfileViewModel> TopRatedAvailableProfiles(string skill, string location);
        bool Promote(PromoteJobViewModel model);
        bool Promote(PromotionEnum promotion);
        IQueryable<ScheduleViewModel> Schedules(int id);
        JobApplicationCompanyViewModel Application(int id);
        IQueryable<CountLabel<int>> JobFolders(int id);
        IQueryable<CountLabel<int>> ContractorFolders();
        IQueryable<CountLabel<int>> BenchFolders();
        bool VisitContractor(int id, VisitViewModel model);

        bool InvitePeople(IList<InviteViewModel> model);
        InviteCodeViewModel AcceptInvitation(string code);
        bool InviteContractorToJob(JobInviteViewModel model);
        string BenchOwnerIdById(int benchId);
        ContractorViewModel GetContractorById(int id);
        JobSearchFilterViewModel ContractorFilters(SearchQueryViewModel model);
     }
}