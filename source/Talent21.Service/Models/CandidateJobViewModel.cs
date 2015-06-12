using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Talent21.Service.Models
{
   public class CandidateJobViewModel
    {
       [Required]
       public int JobId{get; set;}

       [Required]
       public string CompanyName{get;set;}

       public string ContactDetails{get;set;}

       public string CompanyDescription{get; set;}

       public string Skills{get; set;}

       public string Experience{get; set;}

       public string Role{get; set;}

    }
}
