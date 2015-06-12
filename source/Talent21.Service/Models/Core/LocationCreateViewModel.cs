using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models.Core
{
    public class LocationCreateViewModel
    {
        public string State { get; set; }

        public string Country { get; set; }

        public string PinCode { get; set; }
    }

    public class LocationViewModel
    {

        public int Id { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PinCode { get; set; }
    }
}
