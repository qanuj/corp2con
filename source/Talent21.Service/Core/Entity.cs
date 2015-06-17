using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talent21.Service.Core
{
    class Entity
    {
        public string IndustryName { get; set; }

        public Data.Core.JobActionEnum Act { get; set; }

        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int JobId { get; set; }
    }
}
