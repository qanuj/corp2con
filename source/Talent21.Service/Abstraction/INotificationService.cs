using Talent21.Data.Core;
using Talent21.Service.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface INotificationService
    {
        void Welcome(string toEmail, string url);
        void GoodBye(string toEmail);
        void PasswordRecovery(string toEmail, string key);
        void ActOnApplication(JobActionEnum act, string email);
    }
}