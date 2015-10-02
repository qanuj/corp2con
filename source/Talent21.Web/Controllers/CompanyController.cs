using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Web.Http.Description;

namespace Talent21.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/company")]
    public class CompanyController : BasicApiController
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("profile")]
        public CompanyViewModel GetCompanyProfile()
        {
            var userId = User.Identity.GetUserId<string>();
            return _service.GetProfile(userId);
        }

        
        [HttpGet]
        [Route("dashboard")]
        public CompanyDashboardViewModel GetDashboard()
        {
            var userId = User.Identity.GetUserId<string>();
            return _service.GetDashboard(userId);
        }

        [HttpGet]
        [Route("profile/{id}")]
        public CompanyViewModel GetCompanyProfileById(int id)
        {
            var companyProfile = _service.GetProfile(id);
            return companyProfile;
        }


        [HttpPost]
        [Route("profile")]
        public HttpResponseMessage AddCompany(CompanyCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("profile")]
        public HttpResponseMessage EditCompany(CompanyEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("profile/{id}")]
        public HttpResponseMessage DeleteCompany([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new IdModel { Id = id })) : Bad(ModelState);
        }

        //Job Related Api.

        [HttpGet]
        [Route("job/paged")]
        public PageResult<JobViewModel> ViewJobs(ODataQueryOptions<JobViewModel> options)
        {
            return Page(_service.Jobs, options);
        }

        [HttpGet]
        [EnableQuery]
        [Route("job/all")]
        public IQueryable<JobViewModel> ViewJobsQuery()
        {
            return _service.Jobs;
        }

        [HttpGet]
        [EnableQuery]
        [Route("job/active")]
        public IQueryable<IdLabel<int>> ViewActiveJobsQuery()
        {
            return _service.ActiveJobs;
        }

        [HttpGet]
        [EnableQuery]
        [Route("schedule/{id}")]
        public IQueryable<ScheduleViewModel> ViewSchedulesQuery([FromUri]int id)
        {
            return _service.Schedules(id);
        }


        [HttpGet]
        [Route("job/{id}")]
        [ResponseType(typeof(JobViewModel))]
        public HttpResponseMessage SingleJob(int id)
        {
            var model = _service.ById(id);
            return model == null ? NotFound() : Ok(model);
        }


        [HttpPost]
        [Route("job")]
        [ResponseType(typeof(JobViewModel))]
        public HttpResponseMessage CreateJob(CreateJobViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job")]
        [ResponseType(typeof(JobViewModel))]
        public HttpResponseMessage EditJob(EditJobViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/publish")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage PublishJob(PublishJobViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Publish(model)) : Bad(ModelState);
        }

        [HttpPost]
        [Route("job/promote/{id}/{promotion}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage PromoteJob([FromUri] int id, PromotionEnum promotion)
        {
            return ModelState.IsValid ? Ok(_service.Promote(new PromoteJobViewModel { Id = id, Promotion = promotion })) : Bad(ModelState);
        }

        [HttpPost]
        [Route("promote/{promotion}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage PromoteProfile(PromotionEnum promotion)
        {
            return ModelState.IsValid ? Ok(_service.Promote(promotion)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/cancel")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage CancelJob(CancelJobViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Cancel(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [ResponseType(typeof(bool))]
        [Route("job/{id}")]
        public HttpResponseMessage DeleteJob([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new IdModel { Id = id })) : Bad(ModelState);
        }

        //Job Application Related Api.

        [HttpGet]
        [Route("job/{id}/applications/paged")]
        public PageResult<JobApplicationCompanyViewModel> ViewJobs(int id, ODataQueryOptions<JobApplicationCompanyViewModel> options)
        {
            return Page(_service.Applications(id), options);
        }

        [HttpGet]
        [Route("job/{id}/folders")]
        public IQueryable<CountLabel<int>> GetFoldersForJob(int id)
        {
            return _service.JobFolders(id);
        }

        [HttpGet]
        [Route("contractor/folders")]
        public IQueryable<CountLabel<int>> GetFoldersForContractors()
        {
            return _service.ContractorFolders();
        }

        [HttpGet]
        [Route("bench/folders")]
        public IQueryable<CountLabel<int>> GetFoldersForBench()
        {
            return _service.ContractorFolders();
        }

        [HttpGet]
        [Route("job/{id}/applications/all")]
        [EnableQuery]
        public IQueryable<JobApplicationViewModel> ViewJobsQuery(int id)
        {
            return _service.Applications(id);
        }

        [HttpGet]
        [Route("job/application/{id}")]
        [ResponseType(typeof(JobApplicationViewModel))]
        public HttpResponseMessage GetJobApplication([FromUri]int id)
        {
            var model = _service.Application(id);
            return model == null ? NotFound() : Ok(model);
        }


        [HttpPut]
        [Route("job/application/{id}/reject")]
        public HttpResponseMessage RejectJobApplication([FromUri]int id)
        {
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new CreateJobApplicationHistoryViewModel { Id = id }, JobActionEnum.Rejected)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/application/{id}/shortlist")]
        public HttpResponseMessage ShortlistJobApplication([FromUri]int id)
        {
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new CreateJobApplicationHistoryViewModel { Id = id }, JobActionEnum.Shortlist)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/application/{id}/move/{folder}")]
        public HttpResponseMessage MoveJobApplication([FromUri]int id, [FromUri]string folder)
        {
            return ModelState.IsValid ? Ok(_service.MoveApplication(new FolderMoveViewModel { Folder = folder, Id = id })) : Bad(ModelState);
        }

        [HttpPut]
        [Route("contractor/{id}/move/{folder}")]
        public HttpResponseMessage AddContractorToFolder([FromUri]int id, [FromUri]string folder)
        {
            return ModelState.IsValid ? Ok(_service.AddContractorToFolder(new FolderMoveViewModel { Folder = folder, Id = id })) : Bad(ModelState);
        }

        [HttpPut]
        [Route("contractor/invite")]
        public HttpResponseMessage AddContractorToFolder(JobInviteViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.InviteContractorToJob(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("bench/invite")]
        public HttpResponseMessage InviteBench(IList<InviteViewModel> model)
        {
            return ModelState.IsValid ? Ok(_service.InvitePeople(model)) : Bad(ModelState);
        }
        //contractor related api

        [HttpPost]
        [Route("search")]
        public PageResult<ContractorSearchResultViewModel> GetContractorsSearch(SearchQueryViewModel model, ODataQueryOptions<ContractorSearchResultViewModel> options)
        {
            return Page(_service.Search(model), options);
        }

        [HttpGet]
        [Route("contractor/{id}")]
        public ContractorViewModel GetContractorProfileById(int id)
        {
            return _service.GetContractorById(id);
        }

        [HttpPost]
        [Route("bench")]
        public PageResult<ContractorSearchResultViewModel> GetContractorsBench(SearchQueryViewModel model, ODataQueryOptions<ContractorSearchResultViewModel> options)
        {
            return Page(_service.Bench(model), options);
        }

        [HttpGet]
        [Route("latest/profiles/{skill}/{location}")]
        [EnableQuery]
        public IQueryable<ContractorSearchResultViewModel> GetLatestProfiles(string skill, string location)
        {
            return _service.LatestProfiles(skill, location);
        }


        [HttpGet]
        [Route("top/profiles/{skill}/{location}")]
        [EnableQuery]
        public IQueryable<AvailableRatedCandidateProfileViewModel> GetTopRatedAvailableProfiles(string skill, string location)
        {
            return _service.TopRatedAvailableProfiles(skill, location);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("contractor/{id}/visit")]
        public HttpResponseMessage VisitContractor([FromUri] int id)
        {
            return Ok(_service.VisitContractor(id, new VisitViewModel
            {
                IpAddress = GetIpAddress(),
                Browser = System.Web.HttpContext.Current.Request.UserAgent,
                Referer = System.Web.HttpContext.Current.Request.UrlReferrer != null ?
                        System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri : string.Empty
            }));
        }

    }
}
