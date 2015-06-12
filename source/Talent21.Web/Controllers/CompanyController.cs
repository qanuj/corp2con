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
    [Route("~/api/v1/company")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _service;
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        public bool Create(string name)
        {
            var company=_service.CreateCompany(name);
            return company.Id > 0;
        } 
    }
}
