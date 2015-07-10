using System.Runtime.InteropServices;
using System.Web.Mvc;
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


        //[Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new FrontEndViewModel();
                if(User.IsInRole("Admin")) model.Role = "Admin";
                else if(User.IsInRole("Contractor")) model.Role = "Contractor";
                else if(User.IsInRole("Company")) model.Role = "Company";
                return View(model);
            }
            return View("Welcome");
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