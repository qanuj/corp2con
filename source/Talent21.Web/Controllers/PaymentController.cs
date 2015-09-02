using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using e10.Shared.Models;
using Newtonsoft.Json;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service;
using Talent21.Web.Results;

namespace Talent21.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly SellingOptions _sellingOptions;

        public PaymentController(ITransactionRepository transactionRepository, SellingOptions sellingOptions, IMemberRepository memberRepository)
        {
            _transactionRepository = transactionRepository;
            _sellingOptions = sellingOptions;
            _memberRepository = memberRepository;
        }


        [Authorize, Route("~/pay/return")]
        public ActionResult Paid(PaymentReceiptViewModel model)
        {
            const string hashSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";

            var transaction = _transactionRepository.ByCode(model.txnid);
            if (transaction == null)
            {
                throw new Exception(string.Format("No such transaction found '{0}'", model.txnid));
            }

            transaction.PaymentCapture = JsonConvert.SerializeObject(new
            {
                model.status,
                model.hash,
                model.txnid,
                model.productinfo,
                model.key,
                model.firstname,
                model.email,
                model.udf1,
                model.udf2,
                model.udf3,
                model.udf4,
                model.udf5,
                model.udf6,
                model.udf7,
                model.udf8,
                model.udf9,
                model.udf10
            });

            var mercHashVarsSeq = hashSeq.Split('|');
            Array.Reverse(mercHashVarsSeq);
            var mercHashString = _sellingOptions.Salt + "|" + model.status;

            foreach (var mercHashVar in mercHashVarsSeq)
            {
                mercHashString += "|";
                mercHashString = mercHashString + (Request.Form[mercHashVar] ?? "");

            }
            var mercHash = Transaction.GenerateHash512(mercHashString).ToLower();
            transaction.IsSuccess = mercHash == model.hash;
            _transactionRepository.SaveChanges();

            return Redirect("/#/billing?status="+model.status);

        }

        [Authorize, Route("~/pay/{code}")]
        public ActionResult Pay(string code)
        {
            var transction = _transactionRepository.ByCode(code);
            var usr = _memberRepository.ByUserId(transction.UserId);

            var amount = transction.Amount.ToString(CultureInfo.InvariantCulture);
            var hashString = _sellingOptions.Key + "|" + transction.Code + "|" + amount + "|" + transction.Name + "|" + usr.FirstName + "|" + usr.Email + "|||||||||||" + _sellingOptions.Salt;
            var opt = new NameValueCollection
            {
                {"key", _sellingOptions.Key},
                {"txnid", transction.Code},
                {"amount", amount},
                {"productinfo", transction.Name},
                {"firstname", usr.FirstName},
                {"phone", usr.Mobile},
                {"email", usr.Email},
                {"surl", Url.Action("Paid","Payment",new {},Request.Url.Scheme)},
                {"furl", Url.Action("Paid","Payment",new {},Request.Url.Scheme)},
                {"service_provider", "payu_paisa"},
                {"hash",Transaction.GenerateHash512(hashString)}
            };
            return new FormSubmitResult(opt, _sellingOptions.PaymentUrl);
        }

    }
}