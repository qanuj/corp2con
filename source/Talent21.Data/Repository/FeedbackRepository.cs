using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Repository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        void Read(int id, bool what);
    }

    public class FeedbackRepository : EfRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }

        public void Read(int id, bool what)
        {
            var entity = ById(id);
            entity.IsRead = what;
            Update(entity);
        }
    }
}