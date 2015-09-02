using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/v1/admin")]
    public class AdminController : BasicApiController
    {
        private readonly ISystemService _service;
        private readonly ICompanyService _companyService;

        public AdminController(ISystemService service, ICompanyService companyService)
        {
            _service = service;
            _companyService = companyService;
        }

        [HttpGet]
        [Route("transaction")]
        public PageResult<Transaction> GetTransactions(ODataQueryOptions<Transaction> options)
        {
            return Page(_service.Transactions(), options);
        }

    }
}