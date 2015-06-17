using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class SkillDeleteViewModel
    {
        [Required]
        public int CandidateId { get; set; }
    }
}
