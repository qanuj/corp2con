using System.Net.Http;
using System.Web.Http;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    /// <summary>
    /// Candidate Api
    /// </summary>
    [Authorize]
    [Route("~/api/v1/candidate")]
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
        public HttpResponseMessage Create(CreateCandidateAndApplyToJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.CreateCandidateAndApplyToJob(model));
            }
            return Bad(ModelState);
        }
    }
}