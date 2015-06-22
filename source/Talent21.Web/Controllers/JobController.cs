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
    /// Job API
    /// </summary>
    [Authorize]
    [RoutePrefix("~/api/v1/job")]
    public class JobController : BasicApiController
    {
        private readonly IJobService _service;
        /// <summary>
        /// Create Instance of Job Controller
        /// </summary>
        /// <param name="service"></param>
        public JobController(IJobService service)
        {
            _service = service;
        }
        /// <summary>
        /// Apply to Job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("apply")]
        public HttpResponseMessage ApplyToJob(JobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.ApplyToJob(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// Cancel Job
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        [HttpCancel]
        [Route("cancel")]
        public HttpResponseMessage CancelJob(CancelJobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.CancelJob(model));
            }
            return Bad(ModelState);
        }
        /// <summary>
        /// Apply to Job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("apply")]
        public HttpResponseMessage ApplyToJob(ApplyJobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.ApplyToJob(model));
            }
            return Bad(ModelState);
        }
        /// <summary>
        /// Revoke Job Application
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpRevoke]
        [Route("revoke")]
        public HttpResponseMessage RevokeJobApplication(RevokeJobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.RevokeJobApplication(model));
            }
            return Bad(ModelState);
        }

    }
}