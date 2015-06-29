using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

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
        /// <summary>
        /// Create Instance of Candidate Controller
        /// </summary>
        /// <param name="service"></param>
        public CandidateController(ICandidateService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("paged")]
        public PageResult<ContractorViewModel> ViewsCandidates(ODataQueryOptions<ContractorViewModel> options)
        {
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
        [Route("create")]
        public HttpResponseMessage AddProfile(ContractorCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage EditProfile(ContractorEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage DeleteProfile(IdModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }


    }
}