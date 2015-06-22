using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    /// <summary>
    /// Used to Update Company jobs
    /// </summary>
    public class UpdateJobApplicationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int JobId { get; set; }

    
    }
}
