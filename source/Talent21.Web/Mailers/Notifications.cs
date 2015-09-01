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
            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage, "Welcome");
            Send(mvcMailMessage, toEmail);
        }

        public void GoodBye(string toEmail)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "GoodBye" };
            PopulateBody(mvcMailMessage, "GoodBye");

            ViewBag.Email = toEmail;
            Send(mvcMailMessage, toEmail);
        }

        public void PasswordRecovery(string toEmail, string key)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Reset Your password : " + Product.Name };
            mvcMailMessage.To.Add(toEmail);
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
            msg1.To.Add(jobApplication.Contractor.Email);
            ViewBag.Email = jobApplication.Contractor.Email;
            PopulateBody(msg1, "Contractor." + act);
            Send(msg1, jobApplication.Contractor.Email);

            //send to company;
            var msg2 = new MvcMailMessage { Subject = string.Format("{3} {4} - Application Status : {0} for {2} - {1}", act, Product.Name, jobApplication.Job.Title, jobApplication.Contractor.FirstName, jobApplication.Contractor.LastName) };
            msg2.To.Add(jobApplication.Job.Company.Email);
            ViewBag.Email = jobApplication.Job.Company.Email;
            PopulateBody(msg2, "Company." + act);
            Send(msg2, jobApplication.Job.Company.Email);

        }
    }
}