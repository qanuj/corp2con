using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public interface ITransactionRepository : IMyRepository<Transaction>
    {
        Task<Transaction> ByCodeAsync(string code);
        Task<int> BalanceAsync(string id);
    }

    public class TransactionRepository : EfMyRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext db,IEventManager eventManager) : base(db,eventManager) { }
        public Task<Transaction> ByCodeAsync(string code)
        {
            return All.FirstOrDefaultAsync(x => x.Code == code);
        }

        public Task<int> BalanceAsync(string id)
        {
            return Mine(id).Any() ? Mine(id).SumAsync(x => x.Credit) : Task.FromResult(0);
        }

        public override IQueryable<Transaction> Mine(string id)
        {
            return All.Where(x => x.IsSuccess).Where(x => x.UserId == id);
        }
    }
}
