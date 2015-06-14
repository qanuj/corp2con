using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Talent21.Service.Models
{
    public class IndustryDeleteViewModel
    {
        [Required]
        public int IndustryId { get; set; }
    }
}
