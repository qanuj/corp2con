using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class JobRepository : EfDictionaryRepository<Job>, IJobRepository

    {
        public JobRepository(DbContext context, IEventManager eventManager) : base( context, eventManager)
        { 
            
        }

        public override IQueryable<Job> All
        {
            get { return base.All.Include(x => x.Skills).Include(x => x.Locations).Include(x => x.Company); }
        }

        public IQueryable<Job> Mine(string userId)
        {
            return base.All.Where(x => x.Company.OwnerId == userId).Include(x => x.Skills).Include(x => x.Locations).Include(x => x.Company);
        }


        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasKey(x => x.Id);
        }


        public IQueryable<Job> MatchingForConctractor(string userId)
        {
            return All;//TODO:search function fix.
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IJobRepository : IDictionaryRepository<Job>
    {
        IQueryable<Job> Mine(string userId);
        IQueryable<Job> MatchingForConctractor(string userId);
    }

}
