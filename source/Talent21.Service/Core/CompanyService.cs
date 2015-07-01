using System;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Linq;

namespace Talent21.Service.Core
{

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;

        public CompanyService(ICompanyRepository companyRepository,
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository, ISkillRepository skillRepository)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
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


            _candidateRepository.Update(entity);
            var rowAffected = _candidateRepository.SaveChanges();
            return rowAffected > 0;
        }

        public bool ApproveCompany(ApproveCompanyViewModel model)
        {
            var entity = _companyRepository.ById(model.CandidateId);


            _companyRepository.Update(entity);
            var rowAffected = _companyRepository.SaveChanges();
            return rowAffected > 0;
        }


        public CreateJobApplicationViewModel CreateJob(CreateJobApplicationViewModel model)
        {
            var job = new Job() { CompanyId = model.CompanyId };

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
            var entity = _companyRepository.ById(jobApplication.CompanyId);
            _companyRepository.SaveChanges();
            return jobApplication;

        }

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

        public string CurrentUserId { set; private get; }

        public CompanyViewModel GetProfile(string userId)
        {
            return Companies.FirstOrDefault(x => x.OwnerId == userId);
        }

        public JobViewModels Create(CreateJobViewModel model)
        {
            var company = FindCompany(CurrentUserId);
            var entity = new Job
            {
                CompanyId = company.Id,
                Description = model.Description,
                Code = model.Code,
                Title = model.Title,
                End = model.End,
                LocationId = model.LocationId,
                Rate = model.Rate,
                Start = model.Start
            };

            var skills = _skillRepository.ById(model.Skills.Select(x => x.Id)).ToList();
            for (var i = 0; i < skills.Count; i++)
            {
                entity.Skills.Add(skills[i]);
            }

            _jobRepository.Create(entity);
            _jobRepository.SaveChanges();

            return Jobs(CurrentUserId).FirstOrDefault(x => x.Id == entity.Id);

        }

        private Company FindCompany(string userId)
        {
            var company = ByOwner(CurrentUserId);
            if(company == null) throw new Exception("Company Not found");
            return company;
        }

        private Company ByOwner(string userId)
        {
            return _companyRepository.All.FirstOrDefault(x => x.OwnerId == userId);
        }

        public JobViewModels Update(EditJobViewModel model)
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
            if(entity == null) return false;

            entity.IsPublished = true;
            entity.Published = DateTime.UtcNow;

            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool Cancel(CancelJobViewModel model)
        {
            var entity = _jobRepository.ById(model.Id);
            if(entity == null) return false;

            entity.IsCancelled = true;
            entity.Cancelled = DateTime.UtcNow;

            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
        }


        public IQueryable<JobViewModels> Jobs(string userId)
        {
            return _jobRepository.All.Where(x => x.Company.OwnerId == userId).Select(x => new JobViewModels
            {
                Applied = x.Applications.Count,
                Company = x.Company.CompanyName,
                IsCancelled = x.IsCancelled,
                Cancelled = x.Cancelled,
                IsPublished = x.IsPublished,
                Published = x.Published,
                Location = x.Location.Title,
                Skills = x.Skills.Select(y=>new SkillDictionaryViewModel{ Code = y.Code,Id=y.Id, Title = y.Title}),
                CompanyId = x.CompanyId,
                Description = x.Description,
                Code = x.Code,
                Title = x.Title,
                End = x.End,
                LocationId = x.LocationId,
                Rate = x.Rate,
                Start = x.Start
            });
        }
    }
}