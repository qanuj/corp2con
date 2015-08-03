using System.Collections.Generic;

namespace Talent21.Data.Core
{
    public class AdvertisementTransaction : Transaction
    {
        public IList<Advertisement> Advertisements { get; set; }
    }
}