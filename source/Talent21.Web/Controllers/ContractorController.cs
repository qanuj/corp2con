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
            _service.CurrentUserId = User.Identity.GetUserId();
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
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("profile")]
        public HttpResponseMessage EditProfile(ContractorEditViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("profile/{id}")]
        public HttpResponseMessage DeleteProfile([FromUri] int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(new IdModel { Id = id })) : Bad(ModelState);
        }


        //Schedule Related Api

        [HttpGet]
        [Route("schedule/paged")]
        public PageResult<ScheduleViewModel> ViewSchedules(ODataQueryOptions<ScheduleViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Schedules, options);
        }

        [HttpGet]
        [EnableQuery]
        [Route("schedule/all")]
        public IQueryable<ScheduleViewModel> ViewSchedulesQuery()
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Schedules;
        }

        [HttpPost]
        [Route("schedule")]
        public HttpResponseMessage AddSchedule(CreateScheduleViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("schedule")]
        public HttpResponseMessage EditSchedule(EditScheduleViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("schedule/{id}")]
        public HttpResponseMessage DeleteSchedule([FromUri] int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(new DeleteScheduleViewModel{ Id=id})) : Bad(ModelState);
        }

        //skill related api.

        [HttpGet]
        [Route("skill/paged")]
        public PageResult<ContractorSkillViewModel> GetSkills(ODataQueryOptions<ContractorSkillViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Skills, options);
        }

        [HttpGet]
        [EnableQuery]
        [Route("skill/all")]
        public IQueryable<ContractorSkillViewModel> GetSkillsQuert()
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.Skills;
        }

        [HttpPost]
        [Route("skill")]
        public HttpResponseMessage AddSkill(ContractorSkillCreateViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("skill")]
        public HttpResponseMessage EditSkill(ContractorSkillEditViewModel model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("skill/{id}")]
        public HttpResponseMessage DeleteSkill([FromUri]int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Delete(new ContractorSkillDeleteViewModel{ Id=id})) : Bad(ModelState);
        }

        [HttpPost]
        [Route("job/{id}/apply")]
        public HttpResponseMessage ApplyToJob(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.Apply(new JobApplicationCreateViewModel {Id=id })) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/application")]
        public PageResult<JobApplicationContractorViewModel> GetJobApplications(ODataQueryOptions<JobApplicationContractorViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.MyApplications(), options);
        }

        [HttpPost]
        [Route("job/application/history")]
        public IQueryable<JobBasedJobApplicationHistoryViewModel> HasAppliedToJobs(IList<int> model)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.ApplicationHistoryByJobIDs(model);
        }

        [HttpGet]
        [Route("transaction")]
        public PageResult<TransactionViewModel> GetTransactions(ODataQueryOptions<TransactionViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.Transactions(), options);
        }
        
        [HttpPut]
        [Route("job/{id}/revoke")]
        public HttpResponseMessage RejectJobApplication([FromUri] int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new CreateJobApplicationHistoryViewModel{Id=id}, JobActionEnum.Revoke)) : Bad(ModelState);
        }

        //Favorite related api
        [HttpGet]
        [Route("job/{id}/favorite")]
        public ContractorViewModel GetContractorFavoriteById(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _service.GetFavorite(id);
        }

        [HttpGet]
        [Route("job/favorite/paged")]
        public PageResult<JobApplicationContractorViewModel> GetFavoriteJobs(ODataQueryOptions<JobApplicationContractorViewModel> options)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return Page(_service.FavoriteJobs(), options);
        }

        [HttpPut]
        [Route("job/{id}/favorite")]
        public HttpResponseMessage FavoriteJob([FromUri] int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new CreateJobApplicationHistoryViewModel { Id = id }, JobActionEnum.Favorite)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("job/{id}/favorite")]
        public HttpResponseMessage DeleteFavoriteJob([FromUri] int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return ModelState.IsValid ? Ok(_service.ActOnApplication(new DeleteJobApplicationHistoryViewModel { Id = id }, JobActionEnum.Favorite)) : Bad(ModelState);
        }

        [HttpGet]
        [Route("job/{id}")]
        [ResponseType(typeof(JobApplicationContractorViewModel))]
        public HttpResponseMessage SingleJob(int id)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            var model = _service.JobById(id);
            return model == null ? NotFound() : Ok(model);
        }

        [HttpGet]
        [EnableQuery]
        [Route("top/employers/{skill}/{location}")]
        public IQueryable<PictureViewModel> TopEmployers(string skill,string location)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _jobService.TopEmployers(skill, location);
        }


        [HttpGet]
        [Route("latest/jobs/{skill}/{location}")]
        [EnableQuery]
        public IQueryable<JobSearchResultViewModel> GetLatestJobs(string skill, string location)
        {
            _service.CurrentUserId = User.Identity.GetUserId();
            return _jobService.TopJobs(skill, location);
        }
    } 
}