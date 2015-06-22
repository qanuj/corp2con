using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateScheduleViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CandidateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.EmailAddress), EmailAddress, Required]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.PhoneNumber), Phone, Required]
        public int Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
    }
}
