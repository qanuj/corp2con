using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Talent21.Service.Models
{
   public class CandidateAddJobViewModel
    {
       [Required]
       public string Name{get; set;}

       public string Experience{get;set;}

    }
}



