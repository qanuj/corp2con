using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using e10.Shared.Security;
using Microsoft.AspNet.Identity;
using Mvc.Mailer;
using Talent21.Data.Core;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using System.Threading.Tasks;
using e10.Shared.Models;
using Talent21.Data.Repository;

namespace Talent21.Web.Mailers
{
    public sealed class Notifications : MailerBase, INotificationService
    {
        private readonly IIdentityEmailMessageService _emailService;
        private readonly IAppSiteConfigRepository _configRepository;

        public Notifications(IIdentityEmailMessageService emailService, IAppSiteConfigRepository configRepository) :
            base()
        {
            _emailService = emailService;
            _configRepository = configRepository;
            MasterName = "_Layout";
        }

        private void Send(MvcMailMessage msg,params string[] to)
        {
            foreach (var eml in to)
            {
                _emailService.SendAsync(new IdentityMessage
                {
                    Subject = msg.Subject,
                    Destination = eml,
                    Body = msg.Body
                });
            }
        }

        private void Send(MvcMailMessage msg, MessageAttachement attachement, params string[] to)
        {
            foreach (var eml in to)
            {
                _emailService.SendAsync(new IdentityMessage
                {
                    Subject = msg.Subject,
                    Destination = eml,
                    Body = msg.Body
                }, attachement);
            }
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

        public void Feedback(FeedbackCreateViewModel model)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "New Feedback :"+Product.Name };
            ViewBag.Feedback = model;
            PopulateBody(mvcMailMessage, "Feedback");
            Send(mvcMailMessage, _configRepository.Config().Notification.Feedback.Split(new [] {','},StringSplitOptions.RemoveEmptyEntries));
        }

        public void SendApplication(JobApplication application, string viewApplicationUrl, FileInfo resume)
        {
            //send to contractor;
            var msg1 = new MvcMailMessage
            {
                Subject =
                    string.Format("Application Received for {0} - {1}", application.Job.Title,
                        application.Job.Company.CompanyName)
            };
            ViewBag.Email = application.Contractor.Email;
            ViewBag.Model = application;
            ViewBag.Subject = "Thank You!";

            PopulateBody(msg1, "Candidate.Application");
            Send(msg1, application.Contractor.Email);

            //send to company;
            var msg2 = new MvcMailMessage
            {
                Subject =
                    string.Format("New Application : {0} for {1} - {2}", application.Contractor.Email,
                        string.IsNullOrWhiteSpace(application.Job.Code) ? application.Job.Title : application.Job.Code,
                        Product.Name)
            };
            ViewBag.Email = application.Job.Company.Email;
            ViewBag.Url = viewApplicationUrl;
            ViewBag.Subject = "New Application Received";
            PopulateBody(msg2, "Company.Application");
            Send(msg2,
                new MessageAttachement
                {
                    Stream = resume.OpenRead(),
                    Name = string.Format("{0}{1}", application.DownloadableName(), resume.Extension)
                },
                application.Job.Company.Email);
        }
    }
}