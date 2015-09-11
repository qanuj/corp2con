using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.UI;
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
            return View("Welcome", new WelcomePageViewModel
            {
                Jobs = _service.LatestJobs(10),
                FeaturedJob = _service.GetFeaturedJob(),
                Companies = _service.GetFeaturedCompanies(20),
                Numbers = _service.GetStats()
            });
        }

        [Route("terms")]
        public ActionResult Terms()
        {
            return View();
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
    }
}