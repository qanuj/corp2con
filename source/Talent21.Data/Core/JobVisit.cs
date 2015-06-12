namespace Talent21.Data.Core
{
    public class JobVisit : Visit
    {
        public Job Job { get; set; }
        public int JobId { get; set; }
    }
}