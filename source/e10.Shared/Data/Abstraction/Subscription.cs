using System;

namespace e10.Shared.Data.Abstraction
{
    public class Subscription : Entity
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Payment Payment { get; set; }
        public int? PaymentId { get; set; }
    }
}