using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Web.UI;
using Talent21.Service.Abstraction;
using Talent21.Web.Models;

namespace Talent21.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobService _service;

        public HomeController(IJobService service)
        {
            _service = service;
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
        

        [Route("welcome")]
        public ActionResult Welcome()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}