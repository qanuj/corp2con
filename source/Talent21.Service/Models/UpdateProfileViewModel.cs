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
