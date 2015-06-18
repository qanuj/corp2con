using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class ApproveCompanyViewModel
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public int CandidateId { get; set; }        

    }
}
