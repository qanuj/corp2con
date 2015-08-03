using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Location : Dictionary
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }

        public IList<Job> Jobs { get; set; } 
    }
}