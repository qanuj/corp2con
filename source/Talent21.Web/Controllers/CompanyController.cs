using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Talent21.Service.Abstraction;
using Talent21.Service.Core;
using Talent21.Service.Models;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/v1/company")]
    public class CompanyController : BasicApiController
    {
        private readonly ICompanyService _service;
        /// <summary>
        /// Create instance of Company Controller
        /// </summary>
        /// <param name="service"></param>
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("paged")]
        public PageResult<CompanyViewModel> ViewIndustries(ODataQueryOptions<CompanyViewModel> options)
        {
            return Page(_service.Companies, options);
        }

        [HttpGet]
        [Route("all")]
        public IQueryable<CompanyViewModel> ViewIndustriesQuery()
        {
            return _service.Companies;
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage AddIndustry(CompanyCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage EditIndustry(CompanyEditViewModel model)
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
