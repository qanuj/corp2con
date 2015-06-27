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
    /// System Api
    /// </summary>
    [Authorize]
    [RoutePrefix("api/v1/system")]
    public class SystemController : BasicApiController
    {
        private readonly ISystemService _service;

        public SystemController(ISystemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("industry/paged")]
        public PageResult<IndustryDictionaryViewModel> ViewIndustries(ODataQueryOptions<IndustryDictionaryViewModel> options)
        {
            return Page(_service.Industries, options);
        }

        [HttpGet]
        [Route("industry/all")]
        public IQueryable<IndustryDictionaryViewModel> ViewIndustriesQuery()
        {
            return _service.Industries;
        }

        [HttpPost]
        [Route("industry/create")]
        public HttpResponseMessage AddIndustry(IndustryDictionaryCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("industry/update")]
        public HttpResponseMessage EditIndustry(IndustryDictionaryEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("industry/delete")]
        public HttpResponseMessage DeleteIndustry(IndustryDeleteViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }


        [HttpGet]
        [Route("skill/paged")]
        public PageResult<SkillDictionaryViewModel> ViewSkills(ODataQueryOptions<SkillDictionaryViewModel> options)
        {
            return Page(_service.Skills, options);
        }

        [HttpGet]
        [Route("skill/all")]
        public IQueryable<SkillDictionaryViewModel> ViewSkillsQuery()
        {
            return _service.Skills;
        }

        [HttpPost]
        [Route("skill/create")]
        public HttpResponseMessage AddSkill(SkillDictionaryCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("skill/update")]
        public HttpResponseMessage EditSkill(SkillDictionaryEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("skill/delete")]
        public HttpResponseMessage DeleteSkill(SkillDeleteViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("location/paged")]
        public PageResult<LocationDictionaryViewModel> ViewLocations(ODataQueryOptions<LocationDictionaryViewModel> options)
        {
            return Page(_service.Locations, options);
        }

        [HttpGet]
        [Route("location/all")]
        public IQueryable<LocationDictionaryViewModel> ViewLocationsQuery()
        {
            return _service.Locations;
        }

        [HttpPost]
        [Route("location/create")]
        public HttpResponseMessage AddLocation(LocationDictionaryCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("location/update")]
        public HttpResponseMessage EditLocation(LocationDictionaryEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("location/delete")]
        public HttpResponseMessage DeleteLocation(LocationDeleteViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Delete(model)) : Bad(ModelState);
        }

    }
}