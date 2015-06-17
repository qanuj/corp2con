using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class CandidateViewScheduleModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name{ get; set;}
    }
}