using System;
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

        public override IQueryable<Contractor> All
        {
            get { return base.All.Include(x => x.Location).Include(x => x.Company).Include(x => x.Skills.Select(y=>y.Skill)); }
        }

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractor>().HasKey(x => x.Id);
            modelBuilder.Entity<Contractor>()
                .HasOptional(x => x.Company)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.CompanyId);
        }

        public IQueryable<Contractor> MatchingCompanyJobs(string userId)
        {
            return All;//TODO:search function fix.
        }

        public override Contractor ById(int id)
        {
            return All.FirstOrDefault(x => x.Id == id);
        }
    }
    public interface IContractorRepository : IRepository<Contractor>
    {
        IQueryable<Contractor> MatchingCompanyJobs(string userId);
    }
}