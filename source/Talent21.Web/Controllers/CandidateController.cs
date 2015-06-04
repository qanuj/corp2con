using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Talent21.Service.Abstraction;

namespace Talent21.Web.Controllers
{
    [Authorize]
    [Route("~/api/v1/candidate")]
    public class CandidateController : ApiController
    {
        private readonly ICandidateService _service;
        public CandidateController(ICandidateService service)
        {
            _service = service;
        }
    }
}
