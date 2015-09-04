using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.AspNet.Identity;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/billing")]
    public class BillingController : BasicApiController
    {
        private readonly ISharedService _service;
        public BillingController(ISharedService service)
        {
            _service = service;
        }

        [HttpPost]
        [ResponseType(typeof(RedirectViewModel))]
        [Route("credits/{num}")]
        public HttpResponseMessage AddCredits(int num)
        {
            if (num <= 0)
            {
                return Ok(new RedirectViewModel { IsError = true, Error = "Credits can't be 0 or less." });
            }
            var code = _service.AddCredits(num, User.Identity.GetUserId());
            return Ok(new RedirectViewModel { Url = "/pay/" + code });
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


        [HttpGet]
        [Route("balance")]
        public int GetBalance()
        {
            return _service.GetBalance(User.Identity.GetUserId());
        }
    }
}