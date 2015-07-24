using System.Data.Entity;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface  IContractorSkillRepository : IRepository<ContractorSkill>{}

    public class ContractorSkillRepository : EfRepository<ContractorSkill>, IContractorSkillRepository
    {
        public ContractorSkillRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }
    }

}