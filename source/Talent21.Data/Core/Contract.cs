using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Contract : Dictionary
    {
        public string Description { get; set; }

        public Job Job { get; set; }
        public int JobId { get; set; }

        public bool IsFilled { get; set; }
        public string Location { get; set; }
    }
}