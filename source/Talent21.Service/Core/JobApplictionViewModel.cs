using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talent21.Service.Core
{
    public class JobApplictionViewModel
    {
        public int Id { get; set; }

        public Data.Core.JobActionEnum Act { get; set; }
    }
}
