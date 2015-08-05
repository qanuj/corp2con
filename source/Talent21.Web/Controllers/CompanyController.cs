using System;
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

        private static string GetIpAddress()
        {
            var ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            ip = !string.IsNullOrEmpty(ip) ? ip.Split(',').FirstOrDefault() : System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ip.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        [HttpGet]
        [Route("profile/{id}")]
        public CompanyViewModel GetCompanyProfileById(int id)
        {
            var companyProfile= _service.GetProfile(id);
            if (companyProfile != null)
            {
                _service.AddView(companyProfile.Id, System.Web.HttpContext.Current.Request.UserAgent, GetIpAddress());
            }
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
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Jobs, options);
        }

        [HttpGet]
        [Route("job/all")]
        public IQueryable<JobViewModel> ViewJobsQuery()
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Jobs;
        }

        [HttpGet]
        [EnableQuery]
        [Route("schedule/{id}")]
        public IQueryable<ScheduleViewModel> ViewSchedulesQuery([FromUri]int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Schedules(id);
        }


        [HttpGet]
        [Route("job/{id}")]
        [ResponseType(typeof(JobViewModel))]
        public HttpResponseMessage SingleJob(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            var model=_service.ById(id);
            return model == null ? NotFound() : Ok(model);
        }


        [HttpPost]
        [Route("job")]
        [ResponseType(typeof(JobViewModel))]
        public HttpResponseMessage CreateJob(CreateJobViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job")]
        [ResponseType(typeof(JobViewModel))]
        public HttpResponseMessage EditJob(EditJobViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/publish")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage PublishJob(PublishJobViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Publish(model)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/promote/{id}/{promotion}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage PromoteJob([FromUri] int id,PromotionEnum promotion)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Promote(new PromoteJobViewModel { Id = id, Promotion = promotion })) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/cancel")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage CancelJob(CancelJobViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Cancel(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [ResponseType(typeof(bool))]
        [Route("job/{id}")]
        public HttpResponseMessage DeleteJob([FromUri] int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(new IdModel{Id=id})) : Bad(ModelState);
        }

        //Job Application Related Api.

        [HttpGet]
        [Route("job/{id}/applications/paged")]
        public PageResult<JobApplicationViewModel> ViewJobs(int id,ODataQueryOptions<JobApplicationViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Applications(id), options);
        }

        [HttpGet]
        [Route("job/{id}/applications/all")]
        public IQueryable<JobApplicationViewModel> ViewJobsQuery(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Applications(id);
        }
       
        [HttpGet]
        [Route("job/application/{id}")]
        [ResponseType(typeof(JobApplicationViewModel))]
        public HttpResponseMessage GetJobApplication([FromUri]int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            var model = _service.Application(id);
            return model == null ? NotFound() : Ok(model);
        }


        [HttpPut]
        [Route("job/application/{id}/reject")]
        public HttpResponseMessage RejectJobApplication(CreateJobApplicationHistoryViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(model, JobActionEnum.Rejected)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/application/{id}/shortlist")]
        public HttpResponseMessage ShortlistJobApplication(CreateJobApplicationHistoryViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(model, JobActionEnum.Shortlist)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/application/{id}/move/{folder}")]
        public HttpResponseMessage MoveJobApplication(MoveJobApplicationViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.MoveApplication(model)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("transaction")]
        public PageResult<TransactionViewModel> GetTransactions(ODataQueryOptions<TransactionViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Transactions(), options);
        }

        //contractor related api

        [HttpPost]
        [Route("search")]
        public PageResult<ContractorSearchResultViewModel> GetContractorsSearch(SearchQueryViewModel model, ODataQueryOptions<ContractorSearchResultViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Search(model), options);
        }

        [HttpGet]
        [Route("latest/profiles/{skill}/{location}")]
        public IQueryable<ContractorSearchResultViewModel> GetLatestProfiles(string skill, string location)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.LatestProfiles(skill, location);
        }


        [HttpGet]
        [Route("top/profiles/{skill}/{location}")]
        public IQueryable<AvailableRatedCandidateProfileViewModel> GetTopRatedAvailableProfiles(string skill, string location)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.TopRatedAvailableProfiles(skill, location);
        }
    }
}
