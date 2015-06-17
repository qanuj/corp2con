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
        [Required]
        public int CompanyId { get; set; }

        public int JobId { get; set; }

    
    }
}
