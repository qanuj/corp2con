using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Security
{
    public class EmailService : IIdentityEmailMessageService
    {
        public async Task SendAsync(IdentityMessage msg)
        {
            var fromAddress = new MailAddress("donotreply@e10.in", "Do Not Reply Fb Hire");
            var toAddress = new MailAddress(msg.Destination);
            const string fromPassword = "bj%YkV5I5}HlW?Yj";
            const string subject = "Reset Password : Faster Better Hire";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using(var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = msg.Body.IndexOf("<", System.StringComparison.Ordinal) > -1,
                Body = msg.Body
            })
            {
                await smtp.SendMailAsync(message);
                Console.WriteLine("Sent Email");
            }
        }
    }
}