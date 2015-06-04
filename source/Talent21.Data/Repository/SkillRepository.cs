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
     public class SkillRepository : EfRepository<Skill>, ISkillRepository
{
    public SkillRepository(DbContext context, IEventManager eventManager)
        : base(context, eventManager)
    {
    }
}

public interface ISkillRepository : IRepository<Skill>
{

}
}
