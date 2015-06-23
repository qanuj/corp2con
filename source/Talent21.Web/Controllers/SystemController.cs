using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using Talent21.Service.Models.Core;

namespace Talent21.Web.Controllers
{
    /// <summary>
    /// System Api
    /// </summary>
    [Authorize]
    [RoutePrefix("~/api/v1/system")]
    public class SystemController : BasicApiController
    {
        private readonly ISystemService _service;
        /// <summary>
        /// Create Instance of System Controller
        /// </summary>
        /// <param name="service"></param>
        public SystemController(ISystemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a Syetem Record and Apply to Job
        /// </summary>
        /// <param name="model">Model with System Name and Job Id, more fields to come</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddIndustry(AddIndustryViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.AddIndustry(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage EditIndustry(EditIndustryViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.EditIndustry(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteIndustry(DeleteIndustryViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.DeleteIndustry(model));
            }
            return Bad(ModelState);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ViewIndustry(IndustryViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.ViewIndustry(model));
            }
            return Bad(ModelState);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("skill")]
        public HttpResponseMessage AddSkill(AddSkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.AddSkill(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("skill")]
        public HttpResponseMessage EditSkill(EditSkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.EditSkill(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("schedule")]
        public HttpResponseMessage DeleteSkill(DeleteSkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.DeleteSkill(model));
            }
            return Bad(ModelState);
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("skill")]
        public HttpResponseMessage ViewSkill(SkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.ViewSkill(model));
            }
            return Bad(ModelState);
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("location")]
        public HttpResponseMessage AddLocation(LocationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.AddLocation(model));
            }
            return Bad(ModelState);
        }


    }
}