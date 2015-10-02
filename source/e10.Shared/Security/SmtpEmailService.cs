using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using e10.Shared.Models;
using e10.Shared.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;
using SendGrid;

namespace e10.Shared.Security
{
    public class SmtpEmailService : IIdentityEmailMessageService
    {
        private readonly IEmailConfigProvider _config;
        private readonly ILogger _logger;
        public SmtpEmailService(IEmailConfigProvider config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendAsync(IdentityMessage msg)
        {
            var fromAddress = new MailAddress(_config.From, _config.Name);
            var toAddress = new MailAddress(msg.Destination);
            const string subject = "Reset Password : Corp2Con";

            var smtp = new SmtpClient {
                Host = _config.Server,
                Port = _config.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _config.Password)
            };
            using(var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = msg.Body.IndexOf("<", StringComparison.Ordinal) > -1,
                Body = msg.Body
            })
            {
                await smtp.SendMailAsync(message);
                _logger.WriteInformation(string.Format("Email '{0}' Sent to {1}", msg.Subject, msg.Destination));
            }
        }
        public Task SendAsync(IdentityMessage message, params MessageAttachement[] attachments)
        {
            throw new NotImplementedException();
        }
    }
}