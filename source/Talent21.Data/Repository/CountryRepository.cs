using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> ByCodeAsync(string code);
    }

    public class CountryRepository : EfRepository<Country>, ICountryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public CountryRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
        public Task<Country> ByCodeAsync(string code)
        {
            return All.FirstOrDefaultAsync(x => x.Code == code);
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
        }
    }
}