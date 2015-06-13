using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Collections.Generic;


namespace Talent21.Service.Models
{
    public class CandidateCreateViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }

        [Required]
        public string Skills { get; set; }

        [Required]
        public string Experience { get; set; } //in Years and Months
    }
}
