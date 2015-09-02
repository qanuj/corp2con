using System;

namespace e10.Shared.Data.Abstraction
{
    public class Advertisement : Entity
    {
        public PromotionEnum Promotion { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public AdvertisementTransaction Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}