using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Web.Http.Description;

namespace Talent21.Web.Controllers
{
    /// <summary>
    /// Contractor Api
    /// </summary>
    [Authorize]
    [RoutePrefix("api/v1/contractor")]
    public class ContractorController : BasicApiController
    {
        private readonly IContractorService _service;
        private readonly IJobService _jobService;

        public ContractorController(IContractorService service, IJobService jobService)
        {
            _service = service;
            _jobService = jobService;
        }

        [HttpGet]
        [Route("dashboard")]
        public ContractorDashboardViewModel GetDashboard()
        {
            var userId = User.Identity.GetUserId<string>();
            return _service.GetDashboard(userId);
        }
        
        [HttpGet]
        [Route("paged")]
        public PageResult<ContractorViewModel> GetContractors(ODataQueryOptions<ContractorViewModel> options)
        {
            return Page(_service.Contractors, options);
        }

        [HttpGet]
        [Route("all")]
        [EnableQuery]
        public IQueryable<ContractorViewModel> GetContractorsQuery()
        {
            return _service.Contractors;
        }

        [HttpGet]
        [Route("profile")]
        public ContractorViewModel GetContractorProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId<string>();
                return _service.GetProfile(userId);
            }
            return null;
        }


        [HttpGet]
        [Route("profile/{id}")]
        public ContractorViewModel GetContractorProfileById(int id)
        {
            return _service.GetFavorite(id);
        }

        [HttpPost]
        [Route("profile")]
        public HttpResponseMessage AddProfile(ContractorCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("profile")]
        public HttpResponseMessage EditProfile(ContractorEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("profile/{id}")]
        public HttpResponseMessage DeleteProfile([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new IdModel { Id = id })) : Bad(ModelState);
        }


        //Schedule Related Api

        [HttpGet]
        [Route("schedule/paged")]
        public PageResult<ScheduleViewModel> ViewSchedules(ODataQueryOptions<ScheduleViewModel> options)
        {
            return Page(_service.Schedules, options);
        }

        [HttpGet]
        [EnableQuery]
        [Route("schedule/all")]
        public IQueryable<ScheduleViewModel> ViewSchedulesQuery()
        {
            return _service.Schedules;
        }

        [HttpPost]
        [Route("schedule")]
        public HttpResponseMessage AddSchedule(CreateScheduleViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("schedule")]
        public HttpResponseMessage EditSchedule(EditScheduleViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("schedule/{id}")]
        public HttpResponseMessage DeleteSchedule([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new DeleteScheduleViewModel{ Id=id})) : Bad(ModelState);
        }

        //skill related api.

        [HttpGet]
        [Route("skill/paged")]
        public PageResult<ContractorSkillViewModel> GetSkills(ODataQueryOptions<ContractorSkillViewModel> options)
        {
            return Page(_service.Skills, options);
        }

        [HttpGet]
        [EnableQuery]
        [Route("skill/all")]
        public IQueryable<ContractorSkillViewModel> GetSkillsQuert()
        {
            return _service.Skills;
        }

        [HttpPost]
        [Route("skill")]
        public HttpResponseMessage AddSkill(ContractorSkillCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("skill")]
        public HttpResponseMessage EditSkill(ContractorSkillEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("skill/{id}")]
        public HttpResponseMessage DeleteSkill([FromUri]int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new ContractorSkillDeleteViewModel{ Id=id})) : Bad(ModelState);
        }

        [HttpPost]
        [Route("job/{id}/apply")]
        public HttpResponseMessage ApplyToJob(int id)
        {
           return ModelState.IsValid ? Ok(_service.Apply(new JobApplicationCreateViewModel { Id = id },
               Url.Content("~/account/login?returnUrl=/%23/applications"),
               UploadController.FindStorageRoot())) : Bad(ModelState);
        }

        [HttpPost]
        [Route("job/decline")]
        public HttpResponseMessage DeclineInvitationToJob(JobDeclineViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Decline(model)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/application")]
        public PageResult<JobApplicationContractorViewModel> GetJobApplications(ODataQueryOptions<JobApplicationContractorViewModel> options)
        {
            return Page(_service.MyApplications(), options);
        }

        [HttpPost]
        [Route("job/application/history")]
        public IQueryable<JobBasedJobApplicationHistoryViewModel> HasAppliedToJobs(IList<int> model)
        {
            return _service.ApplicationHistoryByJobIDs(model);
        }

        
        [HttpPut]
        [Route("job/{id}/revoke")]
        public HttpResponseMessage RejectJobApplication([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new CreateJobApplicationHistoryViewModel{Id=id}, JobActionEnum.Revoke)) : Bad(ModelState);
        }

        //Favorite related api
        [HttpGet]
        [Route("job/{id}/favorite")]
        public ContractorViewModel GetContractorFavoriteById(int id)
        {
            return _service.GetFavorite(id);
        }

        [HttpGet]
        [Route("job/favorite/paged")]
        public PageResult<JobApplicationContractorViewModel> GetFavoriteJobs(ODataQueryOptions<JobApplicationContractorViewModel> options)
        {
            return Page(_service.FavoriteJobs(), options);
        }

        [HttpPut]
        [Route("job/{id}/favorite")]
        public HttpResponseMessage FavoriteJob([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new CreateJobApplicationHistoryViewModel { Id = id }, JobActionEnum.Favorite)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("job/{id}/favorite")]
        public HttpResponseMessage DeleteFavoriteJob([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new DeleteJobApplicationHistoryViewModel { Id = id }, JobActionEnum.Favorite)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/{id}")]
        [ResponseType(typeof(JobApplicationContractorViewModel))]
        public HttpResponseMessage SingleJob(int id)
        {
            var model = _service.JobById(id);
            return model == null ? NotFound() : Ok(model);
        }

        [HttpGet]
        [EnableQuery]
        [Route("top/employers/{skill}/{location}")]
        public IQueryable<PictureViewModel> TopEmployers(string skill,string location)
        {
            return _jobService.TopEmployers(skill, location);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("company/{id}/visit")]
        public HttpResponseMessage VisitContractor([FromUri] int id)
        {
            return Ok(_service.VisitCompany(id, new VisitViewModel
            {
                IpAddress = GetIpAddress(),
                Browser = System.Web.HttpContext.Current.Request.UserAgent,
                Referer = System.Web.HttpContext.Current.Request.UrlReferrer != null ?
                        System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri : string.Empty
            }));
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("job/{id}/visit")]
        public HttpResponseMessage VisitJob([FromUri] int id)
        {
            return Ok(_service.VisitJob(id, new VisitViewModel
            {
                IpAddress = GetIpAddress(),
                Browser = System.Web.HttpContext.Current.Request.UserAgent,
                Referer = System.Web.HttpContext.Current.Request.UrlReferrer != null ?
                        System.Web.HttpContext.Current.Request.UrlReferrer.AbsoluteUri : string.Empty
            }));
        }

        [HttpGet]
        [Route("latest/jobs/{skill}/{location}")]
        [EnableQuery]
        public IQueryable<JobSearchResultViewModel> GetLatestJobs(string skill, string location)
        {
            return _jobService.TopJobs(skill, location);
        }


        [HttpPost]
        [Route("promote/{promotion}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage PromoteProfile(PromotionEnum promotion)
        {
            return ModelState.IsValid ? Ok(_service.Promote(promotion)) : Bad(ModelState);
        }
    } 
}