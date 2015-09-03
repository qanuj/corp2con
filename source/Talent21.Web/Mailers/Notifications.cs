using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e10.Shared.Security;
using Microsoft.AspNet.Identity;
using Mvc.Mailer;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Threading.Tasks;

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

        private void Send(MvcMailMessage msg, string to)
        {
            _emailService.SendAsync(new IdentityMessage()
            {
                Subject = msg.Subject,
                Destination = to,
                Body = msg.Body
            });
        }

        public void Welcome(string toEmail, string url, string role)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Welcome  to " + Product.Name };
            ViewBag.Url = url;
            ViewBag.UserName = toEmail;
            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage, string.Format("{0}.Welcome", role));
            Send(mvcMailMessage, toEmail);
        }

        public void PasswordRecovery(string toEmail, string resetUrl)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Reset Your password : " + Product.Name };
            ViewBag.ResetUrl = resetUrl;
            ViewBag.UserName = toEmail;

            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage, "PasswordRecovery");
            Send(mvcMailMessage, toEmail);
        }

        public void ActOnApplication(JobApplication jobApplication, JobActionEnum act)
        {
            if (act == JobActionEnum.Favorite) return;
            if (act == JobActionEnum.Reported) return;

            //send to contractor;
            var msg1 = new MvcMailMessage { Subject = string.Format("Application Status : {0} for {2} - {1}", act, Product.Name, jobApplication.Job.Title) };

            if (act == JobActionEnum.Invited){
                msg1.Subject = string.Format("Invitation : {0} - {1}", jobApplication.Job.Title,jobApplication.Job.Company.CompanyName);
            }

            ViewBag.Email = jobApplication.Contractor.Email;
            ViewBag.Application = jobApplication;

            PopulateBody(msg1, "Contractor." + act);
            Send(msg1, jobApplication.Contractor.Email);

            if (act == JobActionEnum.Invited) return; //dont send to company if invited.

            //send to company;
            var msg2 = new MvcMailMessage { Subject = string.Format("{3} {4} - Application Status : {0} for {2} - {1}", act, Product.Name, jobApplication.Job.Title, jobApplication.Contractor.FirstName, jobApplication.Contractor.LastName) };
            ViewBag.Email = jobApplication.Job.Company.Email;
            PopulateBody(msg2, "Company." + act);
            Send(msg2, jobApplication.Job.Company.Email);

        }

        public void Invite(IEnumerable<InviteCodeViewModel> invitees, string by)
        {
            Parallel.ForEach(invitees, inx =>
            {
                var msg2 = new MvcMailMessage { Subject = string.Format("{0} has invited you to {1}", by, Product.Name) };
                ViewBag.By = by;
                ViewBag.Email = inx.Email;
                ViewBag.Invite = inx;
                PopulateBody(msg2, "Invite");
                Send(msg2, inx.Email);
            });
        }
    }
}