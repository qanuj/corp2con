using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using e10.Shared.Security;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/v1/admin")]
    public class AdminController : BasicApiController
    {
        private readonly ISystemService _service;
        private readonly ApplicationUserManager _userManager;

        public AdminController(ISystemService service,ApplicationUserManager userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("transaction")]
        public PageResult<Transaction> GetTransactions(ODataQueryOptions<Transaction> options)
        {
            return Page(_service.Transactions(), options);
        }

        [HttpGet]
        [Route("transaction/{id}")]
        public InvoiceViewModel GetTransactionById([FromUri]int id)
        {
            return _service.TransactionById(id);
        }


        [HttpPost]
        [Route("gift")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage GiftCredits(GiftViewModel model)
        {
            return !ModelState.IsValid ? Bad(ModelState) : Ok(_service.SendGift(model));
        }


        [HttpGet]
        [Route("profile")]
        public ProfileViewModel GetProfile()
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (user == null) return null;
            return new ProfileViewModel
            {
                Email = user.Email,
                Hash = _service.Hash(user.Email)
            };
        }
    }
}