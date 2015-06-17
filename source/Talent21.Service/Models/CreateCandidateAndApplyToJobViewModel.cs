using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Talent21.Service.Models
{
    /// <summary>
    /// Candidate Create and Job Application Information
    /// </summary>
    public class CreateCandidateAndApplyToJobViewModel
    {
        [Required]
        public int CandidateId { get; set; }

       
        /// <summary>
        /// Name of the Candidate
        /// </summary>
        public string Name { get;set; }

        /// <summary>
        /// JobId to Apply to
        /// </summary>
        public int JobId { get; set; }
    }
}
