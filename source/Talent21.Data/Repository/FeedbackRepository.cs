using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Repository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {

    }

    public class FeedbackRepository : EfRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }
    }
}