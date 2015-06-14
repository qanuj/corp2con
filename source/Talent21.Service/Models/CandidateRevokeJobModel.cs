using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class CandidateRevokeJobModel
    {
        public int JobId { get; set; }

        public bool RevokeStatus { get; set; }

        public string RevokeReason { get; set; }
    }
}
