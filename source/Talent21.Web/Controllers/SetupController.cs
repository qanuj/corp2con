using e10.Shared.Security;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using e10.Shared.Data.Abstraction;
using e10.Shared.Util;
using Microsoft.AspNet.Identity;
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

        private const string SuperAdminEmail = "a@e10.in";
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

        public async Task<ActionResult> Verify()
        {
            if (!_roleManager.RoleExists(AccountController.Admin)){ await _roleManager.CreateRolesAsync(new [] {AccountController.Admin});}

            var adminUsers = _userManager.FindByRole(AccountController.Admin);
            if (adminUsers.Any()) return Json("Success", JsonRequestBehavior.AllowGet);

            var adminUser = await _userManager.FindByEmailAsync(SuperAdminEmail);
            if (adminUser == null)
            {
                adminUser = new User { UserName = SuperAdminEmail, Email = SuperAdminEmail };
                var pwd = Randomizer.Generate(10);
                var result = await _userManager.CreateAsync(adminUser, pwd);
                if (!result.Succeeded){ return Json(result, JsonRequestBehavior.AllowGet); }
            }

            var roleResult=await _userManager.AddToRoleAsync(adminUser.Id, AccountController.Admin);

            return !roleResult.Succeeded ? Json(roleResult, JsonRequestBehavior.AllowGet) : Json("Success", JsonRequestBehavior.AllowGet);

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
