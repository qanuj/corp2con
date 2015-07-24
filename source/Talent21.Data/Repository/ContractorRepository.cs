using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using System.Linq;

namespace Talent21.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractorRepository : EfRepository<Contractor>, IContractorRepository 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public ContractorRepository (DbContext context , IEventManager eventManager ) : base (context, eventManager)
        { 
        
        }

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractor>().HasKey(x => x.Id);
        }

        public IQueryable<Contractor> MatchingCompanyJobs(string userId)
        {
            return All;//TODO:search function fix.
        }
    }
    public interface IContractorRepository : IRepository<Contractor>
    {
        IQueryable<Contractor> MatchingCompanyJobs(string userId);
    }
}