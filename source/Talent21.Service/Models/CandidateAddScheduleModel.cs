using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    public class CandidateAddScheduleModel
    {
        /// <summary>
        /// 
        /// </summary>
       
        [Required]
        public string Candidate { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
       
    }
}
