using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    public class ScheduleCreateViewModel
    {
        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int Name { get; set; }

        [DataType(DataType.EmailAddress), EmailAddress, Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber), Phone, Required]
        public int Phone { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }
    }
}
