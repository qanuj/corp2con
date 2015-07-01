using System;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;

        public CompanyService(ICompanyRepository companyRepository,
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository, ISkillRepository skillRepository, IJobApplicationRepository jobApplicationRepository)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public IQueryable<CompanyViewModel> Companies
        {
            get
            {
                return _companyRepository.All.Select(x => new CompanyViewModel
                {
                    Id = x.Id,
                    OwnerId = x.OwnerId,
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

            return Jobs.FirstOrDefault(x => x.Id == entity.Id);

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
            var entity = _jobRepository.ById(model.Id);
            if(entity==null) throw new Exception("Job Not Found");

            entity.Description = model.Description;
            entity.Code = model.Code;
            entity.Title = model.Title;
            entity.End = model.End;
            entity.LocationId = model.LocationId;
            entity.Rate = model.Rate;
            entity.Start = model.Start;
           
            var skills = _skillRepository.ById(model.Skills.Where(x=> entity.Skills.All(y => y.Id != x.Id)).Select(x => x.Id)).ToList();
            for(var i = 0; i < skills.Count; i++){
                entity.Skills.Add(skills[i]);
            }

            var skillDeleted = entity.Skills.Where(x=> model.Skills.All(y => y.Id != x.Id)).ToList();
            for(var i = 0; i < skillDeleted.Count; i++)
            {
                entity.Skills.Remove(skills[i]);
            }

            _jobRepository.Update(entity);
            _jobRepository.SaveChanges();

            return Jobs.FirstOrDefault(x => x.Id == entity.Id);
        }

        public bool Delete(DeleteJobViewModel model)
        {
            _jobRepository.Delete(model.Id);
            var rowsAffested = _jobRepository.SaveChanges();
            return rowsAffested > 0;
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

        public IQueryable<JobViewModels> Jobs
        {
            get
            {
                return _jobRepository.All.Where(x => x.Company.OwnerId == CurrentUserId).Select(x => new JobViewModels
                {
                    Applied = x.Applications.Count,
                    Company = x.Company.CompanyName,
                    IsCancelled = x.IsCancelled,
                    Cancelled = x.Cancelled,
                    IsPublished = x.IsPublished,
                    Published = x.Published,
                    Location = x.Location.Title,
                    Skills = x.Skills.Select(y => new SkillDictionaryViewModel {Code = y.Code, Id = y.Id, Title = y.Title}),
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

        public IQueryable<JobApplicationViewModel> Applications(int id)
        {
            return _jobApplicationRepository.All.Where(x => x.JobId == id).Select(x=> new JobApplicationViewModel
            {
                Actions = x.History.Select(y=> new JobApplicationHistoryViewModel(){ Act = y.Act, Created = y.Created, CreateBy = y.CreatedBy}),
                Id = x.Id,
                Contractor = new ContractorViewModel
                {
                    Id = x.Candidate.Id,
                    About = x.Candidate.About,
                    Email = x.Candidate.Email,
                    ExperienceMonths = x.Candidate.Experience.Months,
                    ExperienceYears = x.Candidate.Experience.Years,
                    Facebook = x.Candidate.Social.Facebook,
                    Google = x.Candidate.Social.Google,
                    LinkedIn = x.Candidate.Social.LinkedIn,
                    LocationId = x.Candidate.LocationId,
                    Mobile = x.Candidate.Mobile,
                    Name = x.Candidate.Name,
                    Rss = x.Candidate.Social.Rss,
                    Twitter = x.Candidate.Social.Twitter,
                    WebSite = x.Candidate.Social.WebSite,
                    Yahoo = x.Candidate.Social.Yahoo,
                    PictureUrl = x.Candidate.PictureUrl,
                    OwnerId = x.Candidate.OwnerId,
                    Rate = x.Candidate.Rate,
                    Skills = x.Candidate.Skills.Select(y => new DictionaryViewModel() { Code = y.Code, Title = y.Title })
                }
            });
        }

        public bool ActOnApplication(CompanyActJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if(entity == null) return false;

            entity.History.Add(new JobApplicationHistory() { Act = model.Act, CreatedBy = CurrentUserId });

            var rowsAffested = _jobApplicationRepository.SaveChanges();
            return rowsAffested > 0;
        }

        public bool ActOnApplication(CreateJobApplicationHistoryViewModel model, JobActionEnum act)
        {
            return ActOnApplication(new CompanyActJobApplicationViewModel(model, act));
        }

        public bool MoveApplication(MoveJobApplicationViewModel model)
        {
            var entity = _jobApplicationRepository.ById(model.Id);
            if(entity == null) return false;

            entity.Folder = model.Folder;

            var rowsAffested = _jobApplicationRepository.SaveChanges();
            return rowsAffested > 0;
        }
    }
}