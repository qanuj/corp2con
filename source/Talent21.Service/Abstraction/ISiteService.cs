using Talent21.Service.Models;

namespace Talent21.Service.Abstraction
{
    public interface ISiteService : IService
    {
        void AddFeedback(FeedbackCreateViewModel model);
    }
}