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
    public class IndustryRepository : EfRepository<Industry>, IIndustryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public IndustryRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        { 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IIndustryRepository: IRepository<Industry>
    { 
    }
}
