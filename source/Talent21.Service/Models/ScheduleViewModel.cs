using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace Talent21.Service.Models
{
    public class ScheduleViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name{ get; set;}
    }
}
