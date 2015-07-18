using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public abstract class Member : Person
    {
        public IList<Subscription> Subscriptions { get; set; }
        public IList<Block> Blocked { get; set; }

        public string Profile { get; set; }
        public Social Social { get; set; }

        public Industry Industry { get; set; }
        public int? IndustryId { get; set; }

        public string About { get; set; }
        public string OwnerId { get; set; }

        protected Member()
        {
            this.Social = new Social();
        }
        
    }
}