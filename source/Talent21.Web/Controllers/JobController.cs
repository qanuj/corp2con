using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
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

    }
}