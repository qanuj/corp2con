using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    public class CandidateRevokeJobModel
    {
        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int JobId { get; set; }
    }
}
