namespace Talent21.Data.Core
{
    public class JobTransaction : Transaction
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }
}