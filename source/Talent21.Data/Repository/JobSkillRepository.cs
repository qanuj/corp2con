using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public class JobSkillRepository : EfRepository<JobSkill>, IJobSkillRepository
    {
        public JobSkillRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
    }

    public interface IJobSkillRepository : IRepository<JobSkill> { }
}