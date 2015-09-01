using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e10.Shared.Security;
using Microsoft.AspNet.Identity;
using Mvc.Mailer;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;

namespace Talent21.Web.Mailers
{
    public sealed class Notifications : MailerBase, INotificationService
    {
        private readonly IIdentityEmailMessageService _emailService;

        public Notifications(IIdentityEmailMessageService emailService) :
            base()
        {
            _emailService = emailService;
            MasterName = "_Layout";
        }

        private void Send(MvcMailMessage msg,string to)
        {
            _emailService.SendAsync(new IdentityMessage()
            {
                Subject = msg.Subject,
                Destination =  to,
                Body = msg.Body
            });
        }

        public void Welcome(string toEmail, string url)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Welcome  to " + Product.Name };
            mvcMailMessage.To.Add(toEmail);
            ViewBag.Url = url;
            ViewBag.UserName = toEmail;
            PopulateBody(mvcMailMessage, "Welcome");
            Send(mvcMailMessage, toEmail);
        }

        public void GoodBye(string toEmail)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "GoodBye" };
            PopulateBody(mvcMailMessage, "GoodBye");
            Send(mvcMailMessage, toEmail);
        }

        public void PasswordRecovery(string toEmail, string key)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Reset Your password : " + Product.Name };
            mvcMailMessage.To.Add(toEmail);
            ViewBag.key = key;
            ViewBag.UserName = toEmail;
            PopulateBody(mvcMailMessage,"PasswordRecovery");
            Send(mvcMailMessage,toEmail);
        }

        public void ActOnApplication(JobActionEnum act, string email)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = string.Format("Your Application Status : {0} - {1}",act,Product.Name) };
            mvcMailMessage.To.Add(email);
            PopulateBody(mvcMailMessage, "Application."+ act);
            Send(mvcMailMessage, email);
        }
    }
}