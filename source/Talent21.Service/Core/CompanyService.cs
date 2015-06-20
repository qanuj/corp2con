using System;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        public CompanyService(ICompanyRepository companyRepository, 
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
        }

        public CreateCompanyViewModel CreateCompany(string name)
        {
            var company = new Company() {Name = name};
            _companyRepository.Create(company);
            SaveChanges();
            return new CreateCompanyViewModel
            {
                Name = company.Name,
                Id = company.Id
            };
        }

        public int SaveChanges()
        {
            return _companyRepository.SaveChanges();
        }


        public AddProfileViewModel UpdateProfile(AddProfileViewModel model)
        {
            var entity = _companyRepository.ById(model.CompanyId);
            entity.Name = model.CompanyName;
            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();
            return model;
        }

        public AddProfileViewModel AddProfile(AddProfileViewModel model)
        {
            var company = new Company
            {
                CompanyId = model.CompanyId,
                CompanyName = model.CompanyName
            };

            _companyRepository.Create(company);
            _companyRepository.SaveChanges();
            return new AddProfileViewModel
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName
            };
        }

        public bool RejectCandidate(RejectCandidateViewModel model)
        {
            var entity = _candidateRepository.ById(model.CandidateId);
            entity.IsRejected = true;
            entity.Cancelled = DateTime.UtcNow;
            _candidateRepository.Update(entity);
            var rowAffected = _candidateRepository.SaveChanges();
            return rowAffected > 0;
        }

        public bool ApproveCompany(ApproveCompanyViewModel model)
        {
            var entity = _companyRepository.ById(model.CandidateId);
            entity.IsApproved = true;
            entity.Cancelled = DateTime.UtcNow;
            _companyRepository.Update(entity);
            var rowAffected = _companyRepository.SaveChanges();
            return rowAffected > 0;
        }

        public CreateJobApplicationViewModel CreateJob(CreateJobApplicationViewModel model)
        {
            var job = new Job() { CompanyId = model.CompanyId };
           // _companyRepository.Create(job);
            _companyRepository.SaveChanges();
            return new CreateJobApplicationViewModel
            {
                CompanyId = job.CompanyId,           
                
            };

        }
        public UpdateJobApplicationViewModel UpdateJob(UpdateJobApplicationViewModel model)
        {
            var entity = _companyRepository.ById(model.CompanyId);
            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();
            return model;
        }

        public bool CancelJob(CancelJobApplicationViewModel model)
        {
            var entity = _jobRepository.ById(model.JobId);
            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;
            _jobRepository.Update(entity);
            var rowAffected = _jobRepository.SaveChanges();
            return rowAffected > 0;
        }

        public DeleteJobApplicationViewModel DeleteJob(DeleteJobApplicationViewModel jobApplication)
        {
            var entity = _companyRepository.ById(jobApplication.CompanyId);
            _companyRepository.Delete(entity);
            return jobApplication;

        }

        public PublishJobApplicationViewModel PublishJob(PublishJobApplicationViewModel jobApplication)
        {
            throw new System.NotImplementedException();
        }


        public UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile)
        {
            throw new NotImplementedException();
        }


        public CreateCompanyViewModel CreateCompany(CreateCompanyViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}