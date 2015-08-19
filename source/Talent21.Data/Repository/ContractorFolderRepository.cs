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

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractorFolder>().HasKey(x => x.Id);
            modelBuilder.Entity<ContractorFolder>().HasRequired(x => x.Company).WithMany(x => x.Folders).HasForeignKey(x => x.CompanyId);
            modelBuilder.Entity<ContractorFolder>().HasRequired(x => x.Contractor).WithMany(x => x.Folders).HasForeignKey(x => x.ContractorId);
        }
    }
}