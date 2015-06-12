using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Talent21.Service.Models
{
    public class CandidateProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int? LocationId { get; set; }

        [DataType(DataType.EmailAddress),EmailAddress,Required]
        public string Email { get; set; }
    }
}
