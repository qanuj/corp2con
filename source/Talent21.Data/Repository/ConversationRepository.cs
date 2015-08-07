using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    public interface IConversationRepository : IRepository<Conversation>
    {

    }
    /// <summary>
    /// 
    /// </summary>
    public class ConversationRepository : EfRepository<Conversation>, IConversationRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public ConversationRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>().HasKey(x => x.Id);
        }
    }
}
