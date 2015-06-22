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
    public class AddSkillViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int CandidateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Skill { get; set; }

    }
}
