using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data;
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
            ContractorRepository.Register(modelBuilder);
            CompanyRepository.Register(modelBuilder);
            JobRepository.Register(modelBuilder);
            ConversationRepository.Register(modelBuilder);
        }
    }
}
