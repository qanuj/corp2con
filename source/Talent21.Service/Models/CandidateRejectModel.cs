using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Talent21.Service.Models
{
    public class CandidateRejectModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        public int CandidateId { get; set; }

        public string Experience { get; set; } //in Years and Months
        
    }
}
