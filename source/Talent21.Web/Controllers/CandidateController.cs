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
    /// <summary>
    /// Candidate Api
    /// </summary>
    [Authorize]
    [RoutePrefix("api/v1/candidate")]
    public class CandidateController : BasicApiController
    {
        private readonly ICandidateService _service;
        private readonly IJobService _jobService;
        /// <summary>
        /// Create Instance of Candidate Controller
        /// </summary>
        /// <param name="service"></param>
        public CandidateController(ICandidateService service, IJobService jobService)
        {
            _service = service;
            _jobService = jobService;
        }

        [HttpGet]
        [Route("paged")]
        public PageResult<ContractorViewModel> ViewsCandidates(ODataQueryOptions<ContractorViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Contractors, options);
        }

        [HttpGet]
        [Route("all")]
        public IQueryable<ContractorViewModel> ViewCandidateQuery()
        {
            return _service.Contractors;
        }

        [HttpGet]
        [Route("profile")]
        public ContractorViewModel GetCandidateProfile()
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
        public HttpResponseMessage AddProfile(ContractorCreateViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("profile")]
        public HttpResponseMessage EditProfile(ContractorEditViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("profile")]
        public HttpResponseMessage DeleteProfile(IdModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }


        //Schedule Related Api

        [HttpGet]
        [Route("schedule/paged")]
        public PageResult<ScheduleViewModel> ViewSchedules(ODataQueryOptions<ScheduleViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Schedules, options);
        }

        [HttpGet]
        [Route("schedule/all")]
        public IQueryable<ScheduleViewModel> ViewSchedulesQuery()
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Schedules;
        }

        [HttpPost]
        [Route("schedule")]
        public HttpResponseMessage AddSchedule(CreateScheduleViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("schedule")]
        public HttpResponseMessage EditSchedule(EditScheduleViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("schedule")]
        public HttpResponseMessage DeleteSchedule(DeleteScheduleViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }

        [HttpPost]
        [Route("job/{id}/apply")]
        public HttpResponseMessage ApplyToJob(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Apply(new JobApplicationCreateViewModel {Id=id })) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/application")]
        public PageResult<JobApplicationViewModel> GetJobApplications(ODataQueryOptions<JobApplicationViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Applications(), options);
        }

        [HttpGet]
        [Route("job/application")]
        public IQueryable<JobApplicationViewModel> GetJobApplicationsQuery()
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Applications();
        }

        [HttpPut]
        [Route("job/application/{id}/revoke")]
        public HttpResponseMessage RejectJobApplication(CreateJobApplicationHistoryViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(model, JobActionEnum.Revoke)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job/application/{id}/favorite")]
        public HttpResponseMessage ShortlistJobApplication(CreateJobApplicationHistoryViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(model, JobActionEnum.Favorite)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/{id}")]
        [ResponseType(typeof(JobSearchResultViewModel))]
        public HttpResponseMessage SingleJob(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            var model = _jobService.ById(id);
            return model == null ? NotFound() : Ok(model);
        }
    } 
}