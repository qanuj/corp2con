using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteScheduleViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CandidateId { get; set; }
    }
}
