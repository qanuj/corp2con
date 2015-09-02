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
        public Transaction ByCode(string code)
        {
            return All.FirstOrDefault(x => x.Code == code);
        }

        public int Balance(string id)
        {
            return Mine(id).Any() ? Mine(id).Sum(x => x.Credit) : 0;
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
        Transaction ByCode(string code);
        int Balance(string id);
    }
}
