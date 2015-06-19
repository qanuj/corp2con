using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SkillRepository : EfRepository<Skill>, ISkillRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public SkillRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISkillRepository : IRepository<Skill>
    {

    }
}
