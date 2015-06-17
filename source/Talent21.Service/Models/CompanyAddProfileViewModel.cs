using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class CompanyAddProfileViewModel
    {

        [Required]
        public string CompnayName { get; set; }

        public DateTime StartUpDate { get; set; }

        public int RegistrationId { get; set;}

        public string CompanyLocation { get; set; }





    }
}
