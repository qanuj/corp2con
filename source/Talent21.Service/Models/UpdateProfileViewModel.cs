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

    }

    public class CompanyCreateViewModel : CompanyEditViewModel
    {
        public string OwnerId { get; set; }
    }

    public class CompanyViewModel : CompanyEditViewModel
    {
        public string PictureUrl { get; set; }
        public DictionaryViewModel Industry { get; set; }

        public string OwnerId { get; set; }
    }


    public class ContractorEditViewModel : PersonViewModel
    {
        public int ExperienceMonths { get; set; }
        public int ExperienceYears { get; set; }
    }

    public class ContractorCreateViewModel : ContractorEditViewModel
    {

        public string OwnerId { get; set; }
    }

    public class ContractorViewModel : ContractorEditViewModel
    {
        internal string OwnerId { get; set; }
        public string PictureUrl { get; set; }
        public int Rate { get; set; }
        public IEnumerable<DictionaryViewModel> Skills { get; set; }
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
