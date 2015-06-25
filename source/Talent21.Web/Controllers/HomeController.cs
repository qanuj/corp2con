using System.Web.Mvc;

namespace Talent21.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                return View();
            }
            return View("Welcome");
        }

        [Route("~/welcome")]
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