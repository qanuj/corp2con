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
    [Authorize]
    [RoutePrefix("api/v1/job")]
    public class JobController : BasicApiController
    {
        private readonly IJobService _service;
        
        public JobController(IJobService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("search")]
        public PageResult<JobSearchResultViewModel> GetContractorsSearch(SearchJobViewModel model, ODataQueryOptions<JobSearchResultViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Search(model), options);
        }
    }
}