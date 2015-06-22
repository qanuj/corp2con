using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateScheduleViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CandidateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? End { get; set; }


    }
}
