using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Talent21.Service.Models
{
    public class CompanyEditViewModel : PersonViewModel
    {
        public int ExperienceMonths { get; set; }
        public int ExperienceYears { get; set; }
    }

    public class CompanyCreateViewModel : ContractorEditViewModel
    {

    }

    public class CompanyViewModel : ContractorEditViewModel
    {

    }


    public class ContractorEditViewModel : PersonViewModel
    {
        public int ExperienceMonths { get; set; }
        public int ExperienceYears { get; set; }
    }

    public class ContractorCreateViewModel : ContractorEditViewModel
    {
        
    }

    public class ContractorViewModel : ContractorEditViewModel
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateProfileViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProfileUrl { get; set; }
       
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class CandidatePublicProfileViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
