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

        public SiteService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public void AddFeedback(FeedbackViewModel model)
        {
            _feedbackRepository.Create(new Feedback
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                Subject = model.Subject
            });
            _feedbackRepository.SaveChanges();
        }
    }
}