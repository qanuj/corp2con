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
        public PageResult<ContractorViewModel> ViewIndustries(ODataQueryOptions<ContractorViewModel> options)
        {
            return Page(_service.Contractors, options);
        }

        [HttpGet]
        [Route("all")]
        public IQueryable<ContractorViewModel> ViewIndustriesQuery()
        {
            return _service.Contractors;
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage AddIndustry(ContractorCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage EditIndustry(ContractorEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage DeleteIndustry(IdModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }


    }
}