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

        public void Welcome(string toEmail, string url,string role)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Welcome  to " + Product.Name };
            ViewBag.Url = url;
            ViewBag.UserName = toEmail;
            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage,string.Format("{0}.Welcome", role));
            Send(mvcMailMessage, toEmail);
        }

        public void PasswordRecovery(string toEmail, string key)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Reset Your password : " + Product.Name };
            ViewBag.key = key;
            ViewBag.UserName = toEmail;

            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage,"PasswordRecovery");
            Send(mvcMailMessage,toEmail);
        }

        public void ActOnApplication(JobApplication jobApplication, JobActionEnum act)
        {
            if (act == JobActionEnum.Favorite) return;
            if (act == JobActionEnum.Reported) return;

            //send to contractor;
            var msg1 = new MvcMailMessage { Subject = string.Format("Application Status : {0} for {2} - {1}", act, Product.Name, jobApplication.Job.Title) };
            ViewBag.Email = jobApplication.Contractor.Email;
            PopulateBody(msg1, "Contractor." + act);
            Send(msg1, jobApplication.Contractor.Email);

            //send to company;
            var msg2 = new MvcMailMessage { Subject = string.Format("{3} {4} - Application Status : {0} for {2} - {1}", act, Product.Name, jobApplication.Job.Title, jobApplication.Contractor.FirstName, jobApplication.Contractor.LastName) };
            ViewBag.Email = jobApplication.Job.Company.Email;
            PopulateBody(msg2, "Company." + act);
            Send(msg2, jobApplication.Job.Company.Email);

        }
    }
}