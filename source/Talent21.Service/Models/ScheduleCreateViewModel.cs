using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class ScheduleCreateViewModel
    {
        [Required]
        public int CandidateId { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public bool IsAvailable { get; set; }
    }
}
