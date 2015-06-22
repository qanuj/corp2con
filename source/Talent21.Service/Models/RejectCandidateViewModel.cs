using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RejectCandidateViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        public int CandidateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Experience { get; set; } //in Years and Months
        
    }
}
