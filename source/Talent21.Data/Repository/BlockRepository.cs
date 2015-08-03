using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;

namespace Talent21.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockRepository : EfRepository<Block>, IBlockRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public BlockRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }

        public override IQueryable<Block> All
        {
            get { return base.All.Where(x=>!x.IsDeleted); }
        }

        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>().HasKey(x => x.Id);
        }
    }
}
