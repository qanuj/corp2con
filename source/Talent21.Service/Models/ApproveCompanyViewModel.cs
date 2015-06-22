using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveCompanyViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CandidateId { get; set; }        

    }
}
