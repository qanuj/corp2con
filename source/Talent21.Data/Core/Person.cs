using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public abstract class Person : Entity, IPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
        public string OwnerId { get; set; }
        public string PictureUrl { get; set; }

        public Social Social { get; set; }
        public Location Location { get; set; }
        public int? LocationId { get; set; }

        public IList<Subscription> Subscriptions { get; set; }
        public IList<Block> Blocked { get; set; }
        protected Person()
        {
            this.Social = new Social();
        }
    }
}
