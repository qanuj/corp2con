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
    public class CreateJobApplicationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string JobId { get; set; }

    }
}
