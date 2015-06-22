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
    public class AddIndustryViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string IndustryName { get; set; }
    
    }
}
