using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
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
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId<string>(); 
                return _service.GetProfile(userId);
            }
            return null;
        }

        [HttpPost]
        [Route("profile")]
        public HttpResponseMessage AddIndustry(CompanyCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("profile")]
        public HttpResponseMessage EditIndustry(CompanyEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("profile")]
        public HttpResponseMessage DeleteIndustry(IdModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
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

        [HttpPut]
        [Route("job/cancel")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage CancelJob(CancelJobViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Cancel(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("job")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage DeleteJob(IdModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
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
    }
}
