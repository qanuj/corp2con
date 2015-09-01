using System.Net.Mail;
using System.Threading.Tasks;
using e10.Shared.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;
using SendGrid;
using System.Net;

namespace e10.Shared.Security
{
    public class SendGridEmailService : IIdentityEmailMessageService
    {
        private readonly IEmailConfigProvider _config;
        private readonly ILogger _logger;
        public SendGridEmailService(IEmailConfigProvider config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            var msg = new SendGridMessage();
            msg.From = new MailAddress(_config.From,_config.Name);

            msg.AddTo(message.Destination);
            msg.Subject = message.Subject;
            msg.Html = message.Body;

            var transportWeb = new Web(_config.SendGridApiKey);
            await transportWeb.DeliverAsync(msg);
            _logger.WriteInformation(string.Format("Email '{0}' Sent to {1}", message.Subject, message.Destination));
        }
    }
}