using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public CompanyViewModel CreateCompany(string name)
        {
            var company = new Company() {Name = name};
            _companyRepository.Create(company);
            SaveChanges();
            return new CompanyViewModel
            {
                Name = company.Name,
                Id = company.Id
            };
        }

        public int SaveChanges()
        {
            return _companyRepository.SaveChanges();
        }


        public CompanyProfileViewModel UpdateProfile(CompanyProfileViewModel profile)
        {
            throw new System.NotImplementedException();
        }

        public CompanyProfileAddModel AddProfile(CompanyProfileAddModel profile)
        {
            throw new System.NotImplementedException();
        }

        public CandidateRejectModel RejectCandidate(CandidateRejectModel jobApplication)
        {
            throw new System.NotImplementedException();
        }

        public CompanyApproveModel ApproveCompany(CompanyApproveModel jobApplication)
        {
            throw new System.NotImplementedException();
        }

        public CompanyCreateJobModel CreateJob(CompanyCreateJobModel jobApplication)
        {
            throw new System.NotImplementedException();
        }

        public CompanyUpdateJobModel UpdateJob(CompanyUpdateJobModel jobApplication)
        {
            throw new System.NotImplementedException();
        }

        public CompanyCancelJobModel CancelJob(CompanyCancelJobModel jobApplication)
        {
            throw new System.NotImplementedException();
        }

        public CompanyDeleteJobModel DeleteJob(CompanyDeleteJobModel jobApplication)
        {
            throw new System.NotImplementedException();
        }

        public CompanyPublishJobModel PublishJob(CompanyPublishJobModel jobApplication)
        {
            throw new System.NotImplementedException();
        }
    }
}