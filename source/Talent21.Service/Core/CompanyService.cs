using System;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Linq;

namespace Talent21.Service.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyRepository"></param>
        /// <param name="jobRepository"></param>
        /// <param name="candidateRepository"></param>
        public CompanyService(ICompanyRepository companyRepository,
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
        }

        public IQueryable<CompanyViewModel> Companies
        {
            get
            {
                return _companyRepository.All.Select(x => new CompanyViewModel
                {
                    Id = x.Id,
                    About = x.About,
                    Email = x.Email,
                    Facebook = x.Social.Facebook,
                    Google = x.Social.Google,
                    LinkedIn = x.Social.LinkedIn,
                    LocationId = x.LocationId,
                    Mobile = x.Mobile,
                    Name = x.Name,
                    Rss = x.Social.Rss,
                    Twitter = x.Social.Twitter,
                    WebSite = x.Social.WebSite,
                    Yahoo = x.Social.Yahoo,
                    PictureUrl = x.PictureUrl,
                    Industry = new DictionaryViewModel() { Code = x.Industry.Code, Title = x.Industry.Title }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CreateCompanyViewModel CreateCompany(string name)
        {
            var company = new Company() { Name = name };
            _companyRepository.Create(company);
            SaveChanges();
            return new CreateCompanyViewModel
            {
                Name = company.Name,
                Id = company.Id
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _companyRepository.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AddProfileViewModel UpdateProfile(AddProfileViewModel model)
        {
            var entity = _companyRepository.ById(model.CompanyId);
            entity.Name = model.CompanyName;
            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RejectCandidate(RejectCandidateViewModel model)
        {
            var entity = _candidateRepository.ById(model.CandidateId);
            //entity.IsRejected = true;
            //entity.Cancelled = DateTime.UtcNow;
            _candidateRepository.Update(entity);
            var rowAffected = _candidateRepository.SaveChanges();
            return rowAffected > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ApproveCompany(ApproveCompanyViewModel model)
        {
            var entity = _companyRepository.ById(model.CandidateId);
            //entity.IsApproved = true;
            //entity.Cancelled = DateTime.UtcNow;
            _companyRepository.Update(entity);
            var rowAffected = _companyRepository.SaveChanges();
            return rowAffected > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UpdateJobApplicationViewModel UpdateJob(UpdateJobApplicationViewModel model)
        {
            var entity = _companyRepository.ById(model.CompanyId);
            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancelJob(CancelJobApplicationViewModel model)
        {
            var entity = _jobRepository.ById(model.JobId);
            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;
            _jobRepository.Update(entity);
            var rowAffected = _jobRepository.SaveChanges();
            return rowAffected > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        public DeleteJobApplicationViewModel DeleteJob(DeleteJobApplicationViewModel jobApplication)
        {
            var entity = _companyRepository.ById(jobApplication.CompanyId);
            _companyRepository.Delete(entity);
            return jobApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        public PublishJobApplicationViewModel PublishJob(PublishJobApplicationViewModel jobApplication)
        {
            var entity = _companyRepository.ById(jobApplication.CompanyId);
            _companyRepository.SaveChanges();
            return jobApplication;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public UpdateProfileViewModel UpdateProfile(UpdateProfileViewModel profile)
        {
            var entity = _companyRepository.ById(profile.Id);
            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();
            return profile; ;
        }

        public CreateCompanyViewModel CreateCompany(CreateCompanyViewModel model)
        {
            throw new NotImplementedException();
        }


        public bool Delete(IdModel model)
        {
            _companyRepository.Delete(model.Id);
            var rowsAffested = _companyRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public CompanyEditViewModel Create(CompanyCreateViewModel model)
        {
            var entity = new Company
            {
                Name = model.Name,

                OwnerId = model.OwnerId,
                Email = model.Email
            };
            _companyRepository.Create(entity);
            _companyRepository.SaveChanges();
            return new CompanyEditViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
        }

        public CompanyEditViewModel Update(CompanyEditViewModel model)
        {
            var entity = _companyRepository.ById(model.Id);
            if(entity == null) return null;

            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.About = model.About;
            entity.LocationId = model.LocationId;
            entity.Mobile = model.Mobile;
            entity.Social = new Social
            {
                Twitter = model.Twitter,
                Facebook = model.Facebook,
                Yahoo = model.Yahoo,
                Google = model.Google,
                LinkedIn = model.LinkedIn,
                Rss = model.Rss,
                WebSite = model.WebSite
            };

            _companyRepository.Update(entity);
            _companyRepository.SaveChanges();

            return model;
        }

        public CompanyViewModel GetProfile(string userId)
        {
            return Companies.FirstOrDefault(x => x.OwnerId == userId);
        }

        public JobViewModel Create(CreateJobViewModel model)
        {
            throw new NotImplementedException();
        }

        public JobViewModel Update(EditJobViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DeleteJobViewModel model)
        {
            throw new NotImplementedException();
        }

        public bool Publish(PublishJobViewModel model)
        {
            var entity = _jobRepository.ById(model.Id);
            if (entity == null) return false;

            entity.IsPublished = true;
            entity.Published = DateTime.UtcNow;

            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool Cancel(CancelJobViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}