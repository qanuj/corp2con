using System;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Subscription : Entity
    {
        public Person Subscriber { get; set; }
        public int SubscriberId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Payment Payment { get; set; }
        public int? PaymentId { get; set; }
    }
}