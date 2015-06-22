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
    public class RevokeJobApplicationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int JobApplicationId { get; set; }
    }
}
