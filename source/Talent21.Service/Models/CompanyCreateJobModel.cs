using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class CompanyCreateJobModel
    {
        [Required]
        public int CompanyId { get; set; }


        public string Name { get; set; }

        [Required]
        public string JobId { get; set; }

    }
}
