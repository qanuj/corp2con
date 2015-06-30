using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using e10.Shared.Data.Abstraction;
using e10.Shared.Security;
using Microsoft.AspNet.Identity;
using Talent21.Service.Abstraction;
using Talent21.Web.Models;

namespace Talent21.Web.Controllers
{
    public class SetupController : Controller
    {
        private readonly ISystemService _service;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly string[] _systemRoles = new string[] { AccountController.Admin, AccountController.Company, AccountController.Contractor };
        public SetupController(ISystemService service, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult Upgrade()
        {
            return Json(_service.Upgrade(), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Index()
        {
            if(await _userManager.Users.AnyAsync())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(SetupViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _roleManager.CreateRolesAsync(_systemRoles);
                if(!User.Identity.IsAuthenticated)
                {
                    await _userManager.CreateGodAsync(model.Email, model.Password);
                }
                else
                {
                    model.Email = User.Identity.Name;
                }
            }
            return View(model);
        }

        public ActionResult Version()
        {
            return Json("v6.12.13.1", JsonRequestBehavior.AllowGet);
        }
    }
}
