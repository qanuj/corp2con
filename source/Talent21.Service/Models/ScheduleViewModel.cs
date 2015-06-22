using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name{ get; set;}
    }
}
