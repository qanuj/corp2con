using System;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class SiteService : ISiteService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly INotificationService _notification;

        public SiteService(IFeedbackRepository feedbackRepository, INotificationService notification)
        {
            _feedbackRepository = feedbackRepository;
            _notification = notification;
        }

        public void AddFeedback(FeedbackCreateViewModel model)
        {
            _feedbackRepository.Create(new Feedback
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                Subject = model.Subject
            });
            _feedbackRepository.SaveChanges();
            _notification.Feedback(model);
        }
    }
}