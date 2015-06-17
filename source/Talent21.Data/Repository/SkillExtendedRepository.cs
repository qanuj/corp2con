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
    public class SkillExtendedRepository : EfRepository<SkillExtended>, ISkillExtendedRepository
    {
        public SkillExtendedRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
    }

    public interface ISkillExtendedRepository : IRepository<SkillExtended>
    {

    }
}
