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
    /// <summary>
    /// 
    /// </summary>
    public class TransactionRepository : EfMyRepository<Transaction>, ITransactionRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public TransactionRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
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

    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionRepository : IMyRepository<Transaction>
    {
        Task<Transaction> ByCodeAsync(string code);
        Task<int> BalanceAsync(string id);
    }
}
