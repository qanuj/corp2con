using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Talent21.Service.Models
{
    public class ScheduleViewModel
    {
        [Required]
        public int CandidateId { get; set; }

        //[DataType(DataType.EmailAddress), EmailAddress, Required]
        //public string Email { get; set; }

        //[DataType(DataType.PhoneNumber), Phone, Required]
        //public int Phone { get; set; }

        public DateTime Date { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

    }
}