using System.Data.Entity;
using System.Linq;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IContractorFolderRepository : IRepository<ContractorFolder>
    {
        IQueryable<ContractorFolder> Mine(string userId);
    }
    public class ContractorFolderRepository : EfRepository<ContractorFolder>, IContractorFolderRepository
    {
        public ContractorFolderRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }

        public IQueryable<ContractorFolder> Mine(string userId)
        {
            return All.Where(x => x.Company.OwnerId == userId);
        }
    }
}