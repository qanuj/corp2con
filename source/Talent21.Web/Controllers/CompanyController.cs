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
          
        [HttpGet]
        [Route("profile")]
        public CompanyViewModel GetCompanyProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId<string>(); 
                return _service.GetProfile(userId);
            }
            return null;
        }

        [HttpPost]
        [Route("profile")]
        public HttpResponseMessage AddIndustry(CompanyCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("profile")]
        public HttpResponseMessage EditIndustry(CompanyEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("profile")]
        public HttpResponseMessage DeleteIndustry(IdModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job")]
        public CompanyViewModel GetJobProfile()
        {
            if(User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId<string>();
                return _service.GetProfile(userId);
            }
            return null;
        }

        [HttpPost]
        [Route("job")]
        public HttpResponseMessage AddJob(PublishJobViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Publish(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("job")]
        public HttpResponseMessage EditJob(EditJobViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("job")]
        public HttpResponseMessage DeleteJob(IdModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }


        public string userId { get; set; }
    }
}
