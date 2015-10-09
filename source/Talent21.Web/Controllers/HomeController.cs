using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.UI;
using e10.Shared.Extensions;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using Talent21.Web.Models;

namespace Talent21.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobService _service;
        private readonly ISiteService _siteService;

        public HomeController(IJobService service, ISiteService siteService)
        {
            _service = service;
            _siteService = siteService;
        }

        //[OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 3600)] //Cache for 1 Hour.
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new FrontEndViewModel();
                if (User.IsInRole("Admin")) model.Role = "Admin";
                else if (User.IsInRole("Contractor")) model.Role = "Contractor";
                else if (User.IsInRole("Company")) model.Role = "Company";
                return View(model);
            }
            return Welcome();
        }

        [Route("welcome")]
        public ActionResult Welcome()
        {
            return View("Welcome", new WelcomePageViewModel
            {
                Jobs = _service.LatestJobs(10),
                FeaturedJob = _service.GetFeaturedJob(),
                Companies = _service.GetTopCompanies(new CompanySearchViewModel { Count = 20, Promotion = PromotionEnum.Global }).ToList(),
                Numbers = _service.GetStats(),
                FeaturedContractor = _service.GetFeaturedContractor()
            });
        }

        [Route("terms")]
        public ActionResult Terms()
        {
            return View();
        }

        [Route("~/jobs")]
        public ActionResult Jobs(string pg = "")
        {
            return View("Jobs", new FrontEndViewModel() { Role = "Public" });
        }

        [Route("contact")]
        [HttpPost]
        public ActionResult Contact(FeedbackViewModel model)
        {
            if(ModelState.IsValid) _siteService.AddFeedback(model);
            ViewBag.Message = "Thank you for your feedback";
            return Index();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [Route("~/go/{id}")]
        public ActionResult Go(string id)
        {
            var reqId = (int)id.ToBase10();
            var req = _service.JobById(reqId);
            if (req == null) return HttpNotFound(id + ":" + reqId);
            return View(new FacebookJobPageViewModel
            {
                Requirement = req,
                Url = Url.Action("Go", "Home", new { id }, Request.Url.Scheme),
                FacebookId = Startup.FacebookAppId
            });
        }

        [Route("~/go/company/{id}")]
        public ActionResult GoCompany(string id)
        {
            var companyId = (int)id.ToBase10();
            var company = _service.CompanyById(companyId);
            if (company == null) return HttpNotFound(id + ":" + companyId);
            return View(new FacebookCompanyPageViewModel
            {
                Company = company,
                Url = Url.Action("Go", "Home", new { id }, Request.Url.Scheme),
                FacebookId = Startup.FacebookAppId
            });
        }
    }
}