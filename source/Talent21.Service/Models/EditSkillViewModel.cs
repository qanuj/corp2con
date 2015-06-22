using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EditSkillViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CandidateId { get; set; }
    }
}
