using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Talent21.Service.Models
{
    public class UpdateProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public int LocationId { get; set; }

        public string Email { get; set; }

        public string ProfileUrl { get; set; }
       
    }


    public class CandidatePublicProfileViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
