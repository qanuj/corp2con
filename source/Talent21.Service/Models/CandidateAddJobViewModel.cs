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
       public int Id{get; set;}

       [Required]
       public string Name{get; set;}

       [DataType(DataType.EmailAddress),EmailAddress,Required]
        public string Email{get; set;}

       [DataType(DataType.PhoneNumber), Phone, Required]
       public int Phone { get; set; }

       public int DateofBirth{get;set;}

       public string Qualification{get;set;}

       public int YearofPassing{get;set;}

       public string CurrentCompanyName{get;set;}

       public string Experience{get;set;}

    }
}



