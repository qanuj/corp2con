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
    class BlockRepository : EfRepository<Block>, IBlockRepository
    {
        public BlockRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {

        }
    }

    public interface IBlockRepository : IRepository<Block>
    { 
    
    }
}
