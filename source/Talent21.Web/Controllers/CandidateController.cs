using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    /// <summary>
    /// Candidate Api
    /// </summary>
    [Authorize]
    [RoutePrefix("~/api/v1/candidate")]
    public class CandidateController : BasicApiController
    {
        private readonly ICandidateService _service;
        /// <summary>
        /// Create Instance of Candidate Controller
        /// </summary>
        /// <param name="service"></param>
        public CandidateController(ICandidateService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a Candidate Record and Apply to Job
        /// </summary>
        /// <param name="model">Model with candidate Name and Job Id, more fields to come</param>
        /// <returns></returns>
        [HttpPost]
        [Route("apply")]
        public HttpResponseMessage Create(CreateCandidateAndApplyToJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.CreateCandidateAndApplyToJob(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage UpdateProfile(UpdateProfileViewModel model)
        {
            if(ModelState.IsValid)
            {
                return Ok(_service.UpdateProfile(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteProfile(DeleteProfileViewModel model)
        {
            if(ModelState.IsValid)
            {
                return Ok(_service.DeleteProfile(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetProfile(int id)
        {
            var profile=_service.GetProfile(id);
            if (profile == null) return NotFound("Profile not found");
            return Ok(profile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all"), EnableQuery]
        public IQueryable<CandidatePublicProfileViewModel> GetProfileQuery()
        {
            return _service.GetProfileQuery();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("paged")]
        public PageResult<CandidatePublicProfileViewModel> GetProfilePaged(ODataQueryOptions<CandidatePublicProfileViewModel> options)
        {
            return Page(_service.GetProfileQuery(),options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("schedule")]
        public HttpResponseMessage AddSchedule(AddScheduleViewModel model)
        {
            if(ModelState.IsValid)
            {
                return Ok(_service.AddSchedule(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("schedule")]
        public HttpResponseMessage UpdateSchedule(UpdateScheduleViewModel model)
        {
            if(ModelState.IsValid)
            {
                return Ok(_service.UpdateSchedule(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("schedule")]
        public HttpResponseMessage DeleteSchedule(DeleteScheduleViewModel model)
        {
            if(ModelState.IsValid)
            {
                return Ok(_service.DeleteSchedule(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("schedule/paged")]
        public PageResult<ScheduleViewModel> GetSchedulePaged(ODataQueryOptions<ScheduleViewModel> options)
        {
            return Page(_service.GetSchedules(), options);
        }

    }
}