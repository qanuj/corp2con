using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Talent21.Service.Abstraction;
using Talent21.Service.Core;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("~/api/v1/company")]
    
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _service;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Create(string name)
        {
            var company=_service.CreateCompany(name);
            return company.Id > 0;
        }    

    }
}
