using System.Collections.Generic;

namespace Talent21.Data.Core
{
    public class AdvertisementTransaction : Transaction
    {
        public Advertisement Advertisement { get; set; }
        public int AdvertisementId { get; set; }
    }
}