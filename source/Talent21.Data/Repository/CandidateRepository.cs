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
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IContractorRepository : IRepository<Contractor>
    { }
}
