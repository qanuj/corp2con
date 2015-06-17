using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    public class CandidateUpdateScheduleModel
    {
        [Required]
        public int CandidateId { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }


    }
}
