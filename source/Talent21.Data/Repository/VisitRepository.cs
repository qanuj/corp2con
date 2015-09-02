using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{

    public interface IVisitRepository : IRepository<Visit>
    {
    }
    public abstract class VisitRepository : EfRepository<Visit>, IVisitRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        protected VisitRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visit>().HasKey(x => x.Id);
        }
    }
}