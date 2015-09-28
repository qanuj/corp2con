using System;
using e10.Shared.Security;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using e10.Shared.Data.Abstraction;
using e10.Shared.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Talent21.Data.Repository;
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
        private readonly INotificationService _notificationService;
        private readonly IAppSiteConfigRepository _configRepository;

        private readonly string[] _systemRoles = { AccountController.Admin, AccountController.Company, AccountController.Contractor };

        private const string SuperAdminEmail = "a@e10.in";
        public SetupController(ISystemService service, 
            IDemoDataService demoDataService,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager, INotificationService notificationService, IAppSiteConfigRepository configRepository)
        {
            _service = service;
            _demoDataService = demoDataService;
            _userManager = userManager;
            _roleManager = roleManager;
            _notificationService = notificationService;
            _configRepository = configRepository;
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ActionResult Upgrade()
        {
            return Json(_service.Upgrade(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Demo()
        {
            return Json(await _demoDataService.BuildAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Impersonate(string email)
        {
            //does user exists
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return HttpNotFound();

            //log off current user
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            AuthenticationManager.SignOut();

            //log in this user
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
            }, identity);

            //back to home
            return RedirectToAction("Index", "Home", new { area = "" });
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

            if (!await _userManager.IsInRoleAsync(adminUser.Id, AccountController.Admin))
            {
                await _userManager.AddToRoleAsync(adminUser.Id, AccountController.Admin);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(adminUser.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = adminUser.Id, code = code }, protocol: Request.Url.Scheme);
            _notificationService.PasswordRecovery(adminUser.Email, callbackUrl);

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
