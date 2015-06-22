using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AddJobApplicationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
       [Required]
       public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [Required]
       public string Name{get; set;}

        /// <summary>
        /// 
        /// </summary>
       public string Experience{get;set;}

    }  
}



