using System;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Advertisement : Entity
    {
        public PromotionEnum Promotion { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public IList<AdvertisementTransaction> Transactions { get; set; }
    }
}