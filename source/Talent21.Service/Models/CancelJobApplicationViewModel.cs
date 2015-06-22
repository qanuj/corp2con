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
    public class CancelJobApplicationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Experience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int JobId { get; set; }
    }
}
