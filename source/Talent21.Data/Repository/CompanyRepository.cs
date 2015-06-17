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
    public class CompanyRepository : EfRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }
    }

    public interface ICompanyRepository : IRepository<Company>
    {

        //void Create(Job job);
    }
}
