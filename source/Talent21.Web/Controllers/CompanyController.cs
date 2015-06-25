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

        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="name">Model with Company Name and more fields to come</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateCompany(CreateCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.CreateCompany(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// Add Profile
        /// </summary>
        /// <param name="ModelState"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddProfile(AddProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.AddProfile(model));
            }
            return Bad(ModelState);
        }
        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage UpdateProfile(UpdateProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.UpdateProfile(model));
            }
            return Bad(ModelState);
        }

        //bool RejectCandidate(RejectCandidateViewModel jobApplication);
        /// <summary>
        /// Reject Candidate
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reject")]
        public HttpResponseMessage RejectCandidate(RejectCandidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.RejectCandidate(model));
            }
            return Bad(ModelState);
        }


        //bool ApproveCompany(ApproveCompanyViewModel jobApplication);
        /// <summary>
        /// Approve Company
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("approve")]
        public HttpResponseMessage ApproveCompany(ApproveCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.ApproveCompany(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// Create Job
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateJob(CreateJobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.CreateJob(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// Update Job
        /// </summary>
        /// <param name="jobApplication"></param>
        [HttpPut]
        public HttpResponseMessage UpdateJob(UpdateJobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.UpdateJob(model));
            }
            return Bad(ModelState);
        }

        /// <summary>
        /// Delete Job
        /// </summary>
        /// <param name="jobApplication"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage DeleteJob(DeleteJobApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.DeleteJob(model));
            }
            return Bad(ModelState);
        }
    }
}
