using System.Collections.Generic;
using Talent21.Data.Core;
using Talent21.Service.Core;
using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface INotificationService
    {
        void Welcome(string toEmail, string url,string role);
        void PasswordRecovery(string toEmail, string key);
        void ActOnApplication(JobApplication application, JobActionEnum act);
        void Invite(IEnumerable<InviteCodeViewModel> invitees,string by);
        void Feedback(FeedbackCreateViewModel model);
    }
}