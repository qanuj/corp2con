using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class CompanyProfileAddModel
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }

        public string Location { get; set; }

        public DateTime SartUpDate { get; set; }

        public int RegistrationId { get; set; }
    }
}
