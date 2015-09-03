using System.Data.Entity;
using e10.Shared.Data;
using e10.Shared.Respository;
using Talent21.Data.Repository;

namespace Talent21.Data
{
    public class ApplicationDataContext : ApplicationDbContext
    {
       public ApplicationDataContext():base("DefaultConnection"){}
       public static ApplicationDataContext Create()
       {
           return new ApplicationDataContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BlockRepository.Register(modelBuilder);
            CountryRepository.Register(modelBuilder);
            TransactionRepository.Register(modelBuilder);
            ContractorRepository.Register(modelBuilder);
            ContractorFolderRepository.Register(modelBuilder);
            CompanyRepository.Register(modelBuilder);
            JobRepository.Register(modelBuilder);
            ConversationRepository.Register(modelBuilder);
            VisitRepository.Register(modelBuilder);
            InviteRepository.Register(modelBuilder);
            FaqRepository.Register(modelBuilder);
        }
    }
}
