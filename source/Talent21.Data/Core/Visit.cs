using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public abstract class Visit : Entity
    {
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Browser { get; set; }
        public string Referer { get; set; }
        public string OperatingSystem { get; set; }
        public bool IsMobile { get; set; }
    }
}