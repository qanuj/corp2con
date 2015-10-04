using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{   
    [AllowAnonymous]
    [RoutePrefix("api/v1/job")]
    public class JobController : BasicApiController
    {
        private readonly IJobService _service;
        private readonly IContractorService _contractorService;
        public JobController(IJobService service, IContractorService contractorService)
        {
            _service = service;
            _contractorService = contractorService;
        }

        [HttpPost]
        [Route("search")]
        public PageResult<JobSearchResultViewModel> GetContractorsSearch(SearchQueryViewModel model, ODataQueryOptions<JobSearchResultViewModel> options)
        {
            return Page(_service.Search(model), options);
        }

        [HttpPost]
        [Route("filters")]
        public JobSearchFilterViewModel GetFiltersForContractorSearch(SearchQueryViewModel model)
        {
            return _service.JobFilters(model);
        }

        [HttpGet]
        [Route("company/{id}/jobs")]
        public PageResult<JobSearchResultViewModel> GetJobsByCompany([FromUri]int id,ODataQueryOptions<JobSearchResultViewModel> options)
        {
            return Page(_service.Search(new SearchQueryViewModel{ CompanyId=id }), options);
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(JobPublicViewModel))]
        public HttpResponseMessage SingleJob(int id)
        {
            var model = _service.JobById(id);
            return model == null ? NotFound() : Ok(model);
        }


        [HttpGet]
        [Route("company/{id}")]
        public CompanyPublicViewModel GetCompanyProfileById(int id)
        {
            var companyProfile = _service.CompanyById(id);
            return companyProfile;
        }


        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("{id}/visit")]
        public HttpResponseMessage VisitJob([FromUri] int id)
        {
            return Ok(_contractorService.VisitJob(id, new VisitViewModel
            {
                IpAddress = GetIpAddress(),
                Browser = System.Web.HttpContext.Current.Request.UserAgent,
                Referer = System.Web.HttpContext.Current.Request.UrlReferrer != null ?
                        System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri : string.Empty
            }));
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("company/{id}/visit")]
        public HttpResponseMessage VisitCompany([FromUri] int id)
        {
            return Ok(_contractorService.VisitCompany(id, new VisitViewModel
            {
                IpAddress = GetIpAddress(),
                Browser = System.Web.HttpContext.Current.Request.UserAgent,
                Referer = System.Web.HttpContext.Current.Request.UrlReferrer != null ?
                         System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri : string.Empty
            }));
        }


    }
}