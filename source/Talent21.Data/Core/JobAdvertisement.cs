namespace Talent21.Data.Core
{
    public class JobAdvertisement : Advertisement
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }
}