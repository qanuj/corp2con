using e10.Shared.Security;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Talent21.Service.Abstraction;
using Talent21.Web.Models;

namespace Talent21.Web.Controllers
{
    public class SetupController : Controller
    {
        private readonly ISystemService _service;
        private readonly IDemoDataService _demoDataService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly string[] _systemRoles = { AccountController.Admin, AccountController.Company, AccountController.Contractor };
        public SetupController(ISystemService service, 
            IDemoDataService demoDataService,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            _service = service;
            _demoDataService = demoDataService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult Upgrade()
        {
            return Json(_service.Upgrade(), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Demo()
        {
            return Json(await _demoDataService.BuildAsync(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Master()
        {
            _demoDataService.BuildMaster();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Roles()
        {
            _demoDataService.BuildMaster();
            await _roleManager.CreateRolesAsync(new []
            {
                AccountController.Company,
                AccountController.Admin,
                AccountController.Contractor
            });

            return Json("Success", JsonRequestBehavior.AllowGet);
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
