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
    public class TransactionRepository : EfRepository<Transaction>, ITransactionRepository
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
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionRepository : IRepository<Transaction>
    {

    }
}
